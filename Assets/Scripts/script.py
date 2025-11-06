import os.path

from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError

# If modifying these scopes, delete the file token.json.
SCOPES = ["https://www.googleapis.com/auth/documents.readonly"]

# The ID of a sample document.
DOCUMENT_ID = "1tjfntQXjK0MULyBZu6L6SRz_EPm4QGrL3QjWYYfN3AI"

X_COORDINATE_COLUMN = 0
Y_COORDINATE_COLUMN = 2
CHARACTER_COLUMN = 1
EMPTY_CELL_CHARACTER = "0"

def parse_row(row):
  """Parses a single row from a table in a document into a dictionary
  Containing x and y coordinates and the character at that coordinate.

  Args:
      row: a single row from a table in the document.
  """

  cells = row.get('tableCells')

  x_coordinate_cell = cells[X_COORDINATE_COLUMN]
  y_coordinate_cell = cells[Y_COORDINATE_COLUMN]
  character_cell = cells[CHARACTER_COLUMN]

  character_value = read_single_cell(character_cell.get('content'))
  x_coordinate_value = int(read_single_cell(x_coordinate_cell.get('content')))
  y_coordinate_value = int(read_single_cell(y_coordinate_cell.get('content')))

  return {"x_coordinate": x_coordinate_value, "y_coordinate": y_coordinate_value, "character": character_value} 

def first_table_from_document(elements):
  """Finds the first table in a list of Structural Elements.

  Args:
      elements: a list of Structural Elements.
  """

  for value in elements:
    if 'table' in value:
      return value.get('table')

  return None

def parse_coordinates(table):
  """Transforms a table from a document into a list of parsed rows containing coordinates and characters.

  Args:
      table: A raw table from document containing coordinates and characters. 
  """

  parsed_rows = []
  maximum_x_coordinate = 0
  maximum_y_coordinate = 0

  for row in table.get('tableRows')[1:]:
    parsed_row = parse_row(row)  
    parsed_rows.append(parsed_row)
    maximum_x_coordinate = max(maximum_x_coordinate, parsed_row.get('x_coordinate'))
    maximum_y_coordinate = max(maximum_y_coordinate, parsed_row.get('y_coordinate'))

  return (parsed_rows, maximum_x_coordinate, maximum_y_coordinate)

def create_empty_matrix(maximum_x_coordinate, maximum_y_coordinate):
  """Creates an empty matrix with the dimensions of the largest x and y coordinates.

  Args:
      maximum_x_coordinate: The largest x coordinate in the dataset.
      maximum_y_coordinate: The largest y coordinate in the dataset.
  """

  empty_matrix = []

  for _ in range(maximum_y_coordinate + 1):
      empty_matrix.append([EMPTY_CELL_CHARACTER] * (maximum_x_coordinate + 1))

  return empty_matrix

def process_coordinate_matrix(parsed_coordinates, maximum_x_coordinate, maximum_y_coordinate):
  """Transforms an unsorted array containing the information at each coordinate
  Into into a matrix where each cell contains the character at that coordinate.

  Args:
      parsed_coordinates: An unsorted array containing the information at each coordinate.
      maximum_x_coordinate: The largest x coordinate in the dataset.
      maximum_y_coordinate: The largest y coordinate in the dataset.
  """

  coordinate_matrix = create_empty_matrix(maximum_x_coordinate, maximum_y_coordinate)

  for parsed_row in parsed_coordinates:
    x_coordinate = parsed_row.get("x_coordinate")
    y_coordinate = parsed_row.get("y_coordinate")
    character = parsed_row.get("character")

    coordinate_matrix[y_coordinate][x_coordinate] = character

  return coordinate_matrix

def write_message_row(row):
  """Writes a single row of a coordinate matrix to the console.

  Args:
      row: a single row of a coordinate matrix.
  """

  for element in row:
      if element != EMPTY_CELL_CHARACTER:
          print(element, end="")
      else:
          print(" ", end="")


def write_message_from_matrix(coordinate_matrix):
  """Writes a message from a coordinate matrix to the console.
  Args:
      coordinate_matrix: matrix containing decrypted message.
  """

  for row in reversed(coordinate_matrix):
      write_message_row(row)
      print("\n")

def read_single_cell(elements):
  """Reads a single cell from a list of Structural Elements
  It gets the text from the first paragraph of the cell

  Args:
      elements: a list of Structural Elements belonging to cell.
  """
  element = elements[0]

  text_run = element.get('paragraph').get('elements')[0].get('textRun')
  if not text_run:
    return ''
  return text_run.get('content').rstrip("\n")

def authenticated_credentials():
  """Authenticates the user and returns the credentials for the Google Docs API.

  Args:
      elements: a list of Structural Elements.
  """

  creds = None
  # The file token.json stores the user's access and refresh tokens, and is
  # created automatically when the authorization flow completes for the first
  # time.
  if os.path.exists("token.json"):
    creds = Credentials.from_authorized_user_file("token.json", SCOPES)
  # If there are no (valid) credentials available, let the user log in.
  if not creds or not creds.valid:
    if creds and creds.expired and creds.refresh_token:
      creds.refresh(Request())
    else:
      flow = InstalledAppFlow.from_client_secrets_file(
          "credentials.json", SCOPES
      )
      creds = flow.run_local_server(port=0)
    # Save the credentials for the next run
    with open("token.json", "w", encoding="utf-8") as token:
      token.write(creds.to_json())

  return creds

def get_document(creds):
  """Gets the required document from the Google Docs API.

  Args:
      creds: Google API authenticated credentials.
  """

  try:
    service = build("docs", "v1", credentials=creds)

    # Retrieve the documents contents from the Docs service.
    return service.documents().get(documentId=DOCUMENT_ID).execute()
  except HttpError as err:
    print(err)
    return err

def main():
  """Gets a document from the google docs api
  Prints the decrypted message from a table to the console.
  """

  document = get_document(authenticated_credentials())

  if document is not None:
    doc_content = document.get('body').get('content')

    coordinate_table = first_table_from_document(doc_content)

    if coordinate_table is None:
        parsed_coordinates = parse_coordinates(coordinate_table)
        final_coordinate_matrix = process_coordinate_matrix(*parsed_coordinates)
        write_message_from_matrix(final_coordinate_matrix)
    else:
        print("No table found in the document.")
     
  else:
    print("No document found with the specified ID.")


if __name__ == "__main__":
  main()