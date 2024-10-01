# Claim API Documentation

This documentation provides information about the Claim API, which allows users to manage insurance claims.

- [Claim API Documentation](#claim-api-documentation)
  - [Create Claim](#create-claim)
    - [Create Claim Request](#create-claim-request)
    - [Create Claim Response](#create-claim-response)
  - [Delete Claim](#delete-claim)
    - [Delete Claim Request](#delete-claim-request)
    - [Delete Claim Response](#delete-claim-response)
  - [Get Claim by ID](#get-claim-by-id)
    - [Get Claim by ID Request](#get-claim-by-id-request)
    - [Get Claim by ID Response](#get-claim-by-id-response)
  - [Get All Claims](#get-all-claims)
    - [Get All Claims Request](#get-all-claims-request)
    - [Get All Claims Response](#get-all-claims-response)

## Create Claim

### Create Claim Request

- **Method**: POST
- **Endpoint**: `/api/v1/claims`
- **Description**: Creates a new insurance claim.
- **Headers**:
  - `Content-Type`: `application/json`
- **Body Parameters**:
  - `coverId` (int, required): The ID of the related cover.
  - `createdDate` (datetime, required): The creation date of the claim. Must be within the period of the related cover.
  - `name` (string, required): The name of the claim.
  - `type` (int, required): The type of claim. This is represented by an integer value corresponding to a specific ClaimType.
  - `damageCost` (decimal, required): Cost of the damage. Must not exceed 100,000.

**Example Request Body**:

```json
{
  "coverId": "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6",
  "created": "2024-09-01T10:00:00Z",
  "name": "Fire Damage",
  "type": 2,
  "damageCost": 5000
}
```

### Create Claim Response

- **Status Codes**:
  - `201 Created`: Claim successfully created.
  - `400 Bad Request`: Invalid input parameters.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response Body**:

```json
{
  "claimId": "123e4567-e89b-12d3-a456-426614174000",
  "coverId": "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6",
  "created": "2024-09-01T10:00:00Z",
  "name": "Fire Damage",
  "type": 2,
  "damageCost": 5000
}
```

## Delete Claim

### Delete Claim Request

- **Method**: DELETE
- **Endpoint**: `/api/v1/claims/{claimId}`
- **Description**: Deletes an existing insurance claim.

### Delete Claim Response

- **Status Codes**:
  - `200 OK`: Claim successfully deleted.
  - `404 Not Found`: Claim not found.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response**:

```
   "569a062f-0130-46d6-a22a-08dce240175e"
```

## Get Claim by ID

### Get Claim by ID Request

- **Method**: GET
- **Endpoint**: `/api/v1/claims/{claimId}`
- **Description**: Retrieves an insurance claim by its ID.

### Get Claim by ID Response

- **Status Codes**:

  - `200 OK`: Claim successfully retrieved.
  - `404 Not Found`: Claim not found.
  - `500 Internal Server Error`: Server error while processing the request.

  **Example Response Body**:

  ```json
  {
    "claimId": "123e4567-e89b-12d3-a456-426614174000",
    "coverId": "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6",
    "created": "2024-09-01T10:00:00Z",
    "name": "Fire Damage",
    "type": 2,
    "damageCost": 5000
  }
  ```

## Get All Claims

### Get All Claims Request

- **Method**: GET
- **Endpoint**: `/api/v1/claims`
- **Description**: Retrieves all insurance claims.

### Get All Claims Response

- **Status Codes**:
  - `200 OK`: Claims successfully retrieved.
  - `500 Internal Server Error`: Server error while processing the request.

**Example Response Body**:

```json
[
  {
    "claimId": "569a062f-0130-46d6-a22a-08dce240175e",
    "coverId": "7f2ebc09-57e9-4c9d-fe55-08dce219470b",
    "created": "2024-09-09T10:00:00",
    "name": "Fire Damage",
    "type": 2,
    "damageCost": 5000.00
  },
  {
    "claimId": "67c47ccf-4d30-4c26-c513-08dce2432b07",
    "coverId": "7f2ebc09-57e9-4c9d-fe55-08dce219470b",
    "created": "2024-09-09T10:00:00",
    "name": "Real Fire Damage",
    "type": 3,
    "damageCost": 5000.00
  },
  {
    "claimId": "67b68a85-0bfa-4bf2-c514-08dce2432b07",
    "coverId": "7f2ebc09-57e9-4c9d-fe55-08dce219470b",
    "created": "2024-09-09T10:00:00",
    "name": "Bad Weather Damage",
    "type": 2,
    "damageCost": 5000.00
  },
  {
    "claimId": "cc99e0b8-d2b3-4564-c515-08dce2432b07",
    "coverId": "7f2ebc09-57e9-4c9d-fe55-08dce219470b",
    "created": "2024-09-09T10:00:00",
    "name": "Collision Damage",
    "type": 0,
    "damageCost": 5000.00
  }
]
```
