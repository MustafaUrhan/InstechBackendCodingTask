# Cover API Documentation

This documentation provides information about the Cover API, which allows users to manage insurance covers.

- [Cover API Documentation](#cover-api-documentation)
  - [Create Cover](#create-cover)
    - [Create Cover Request](#create-cover-request)
    - [Create Cover Responses](#create-cover-responses)
  - [Compute Premium](#compute-premium)
    - [Compute Premium Request](#compute-premium-request)
    - [Compute Premium Response](#compute-premium-response)
  - [Delete Cover](#delete-cover)
    - [Delete Cover Request](#delete-cover-request)
    - [Delete Cover Response](#delete-cover-response)
  - [Get Cover by ID](#get-cover-by-id)
    - [Get Cover by ID Request](#get-cover-by-id-request)
    - [Get Cover by ID Response](#get-cover-by-id-response)
  - [Get All Covers](#get-all-covers)
    - [Get All Covers Request](#get-all-covers-request)
    - [Get All Covers Response](#get-all-covers-response)

## Create Cover

### Create Cover Request

- **Method**: POST
- **Endpoint**: `/api/v1/covers`
- **Description**: Creates a new insurance cover.
- **Headers**:
  - `Content-Type`: `application/json`
- **Body Parameters**:
  - `startDate` (datetime, required): The start date of the coverage (Cannot be in the past)
  - `endDate` The end date of the coverage (Cannot exceed 1 year from StartDate).
  - `type` (int, required): Type of object covered (Yacht, PassengerShip, ContainerShip,BulkCarrier,Tanker)

**Example Request Body**:

```json
{
  "startDate": "2025-01-01",
  "endDate": "2025-06-01",
  "type": 0
}
```

### Create Cover Responses

- `201 Created`: Cover successfully created.

```json
{
  "id": "906ecdab-0f25-4d6e-dcb0-08dce1fefe55",
  "startDate": "2025-01-01T00:00:00",
  "endDate": "2025-06-01T00:00:00",
  "type": 0,
  "premium": 429508.75,
  "createdAt": "2024-10-01T10:54:08.541993+03:00"
}
```

- `400 Bad Request`: Invalid input parameters.

```json
"errors": {
    "EndDate": [
      "Insurance period cannot exceed 1 year"
    ],
    "StartDate": [
      "Start date must be greater than current date"
    ]
  }
```

- `500 Internal Server Error`: Server error while processing the request.

## Compute Premium

- **Method**: POST
- **Endpoint**: `/api/v1/Covers/Compute`
- **Description**: Computes the premium for an insurance cover based on the given start and end dates and the cover type.
- **Headers**:
  - `Content-Type`: `application/json`
- **Body Parameters**:
  - `startDate` (datetime, required): The start date of the coverage
  - `endDate` The end date of the coverage
  - `type` (int, required): Type of object covered (Yacht, PassengerShip, ContainerShip,BulkCarrier,Tanker)

### Compute Premium Request

```json
{
  "startDate": "2025-01-01T00:00:00",
  "endDate": "2025-06-01T00:00:00",
  "type": 0
}
```

### Compute Premium Response

- **Status Codes**:
  - `200 OK`: Premium successfully computed.
  - `400 Bad Request`: Invalid input data (e.g., invalid date range or missing parameters).
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response**:

```
   15000.0
```

## Delete Cover

### Delete Cover Request

- **Method**: DELETE
- **Endpoint**: `/api/v1/covers/{coverId}`
- **Description**: Deletes an existing insurance cover.

### Delete Cover Response

- **Status Codes**:
  - `200 OK`: Cover successfully deleted.
  - `404 Not Found`: Cover not found.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response**:

```
   "569a062f-0130-46d6-a22a-08dce240175e"
```

## Get Cover by ID

### Get Cover by ID Request

- **Method**: GET
- **Endpoint**: `/api/v1/covers/{coverId}`
- **Description**: Retrieves an insurance cover by its ID.

### Get Cover by ID Response

- **Status Codes**:
  - `200 OK`: Cover successfully retrieved.
  - `404 Not Found`: Cover not found.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response Body**:

```json
{
  "id": "906ecdab-0f25-4d6e-dcb0-08dce1fefe55",
  "startDate": "2025-01-01T00:00:00",
  "endDate": "2025-06-01T00:00:00",
  "type": 0,
  "premium": 429508.75,
  "createdAt": "2024-10-01T12:54:08.543"
}
```

## Get All Covers

### Get All Covers Request

- **Method**: GET
- **Endpoint**: `/api/v1/covers`
- **Description**: Retrieves all insurance covers.

### Get All Covers Response

- **Status Codes**:
  - `200 OK`: Covers successfully retrieved.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response Body**:

```json
[
  {
    "id": "906ecdab-0f25-4d6e-dcb0-08dce1fefe55",
    "startDate": "2025-01-01T00:00:00",
    "endDate": "2025-06-01T00:00:00",
    "type": 0,
    "premium": 429508.75,
    "createdAt": "2024-10-01T12:54:08.543"
  },
  {
    "id": "906ecdab-0f25-4d6e-dcb0-08dce1fef054",
    "startDate": "2025-01-01T00:00:00",
    "endDate": "2025-06-01T00:00:00",
    "type": 0,
    "premium": 529508.75,
    "createdAt": "2024-10-01T12:55:08.543"
  }
]
```
