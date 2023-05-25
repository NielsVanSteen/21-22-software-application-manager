# Project .NET Framework

* Naam: Niels Van Steen
* Studentennummer: 0145682-85
* Academiejaar: 21-22
* Klasgroep: INF203A
* Onderwerp: Software Application - (Availability on) OS

## Sprint 3

### Both empty.
```
SELECT "o"."OperatingSystemId", "o"."Description", "o"."Name", "o"."ReleaseDate"
FROM "OperatingSystems" AS "o"
```

### Only name filled in.
```
SELECT "o"."OperatingSystemId", "o"."Description", "o"."Name", "o"."ReleaseDate"
FROM "OperatingSystems" AS "o"
WHERE (@__ToLower_0 = '') OR (instr(lower("o"."Name"), @__ToLower_0) > 0)
```


### Only date filled in.
```
SELECT "o"."OperatingSystemId", "o"."Description", "o"."Name", "o"."ReleaseDate"
FROM "OperatingSystems" AS "o"
WHERE "o"."ReleaseDate" = @__releaseDate_0
```


### Both filled in.
```
SELECT "o"."OperatingSystemId", "o"."Description", "o"."Name", "o"."ReleaseDate"
FROM "OperatingSystems" AS "o"
WHERE ((@__ToLower_0 = '') OR (instr(lower("o"."Name"), @__ToLower_0) > 0)) AND ("o"."ReleaseDate" = @__releaseDate_1)
```


## Sprint 6

### New Developer.

#### Request
POST https://localhost:5001/api/Developers
Content-Type: application/json

{"developerId": 3, "name": "Adobe", "description": "Adobe Inc.", "birthDate": "2000-01-01", "profilePicture": "rider.png", "phoneNumber": "0123 45 56 78", "email": "adobe@contact.com" }

#### Response

HTTP/1.1 201 Created
Date: Sun, 19 Dec 2021 18:22:35 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/api/Developers/3

{
  "developerId": 3,
  "name": "Adobe",
  "description": "Adobe Inc.",
  "birthDate": "2000-01-01T00:00:00",
  "profilePicture": "rider.png",
  "phoneNumber": "0123 45 56 78",
  "email": "adobe@contact.com",
  "developedApplications": [],
  "ratedApplications": [],
  "address": null
}

Response code: 201 (Created); Time: 1940ms; Content length: 245 bytes'

### Updating Developer (Success)

#### Request
PUT https://localhost:5001/api/Developers/3
Content-Type: application/json

{"developerId": 3, "name": "Adobe Inc.", "description": "Adobe Inc.", "birthDate": "2002-01-02", "profilePicture": "intellij.png", "phoneNumber": "9876 54 32 10", "email": "adobe@contact.com" }

#### Response
HTTP/1.1 204 No Content
Date: Sun, 19 Dec 2021 20:30:09 GMT
Server: Kestrel

<Response body is empty>

Response code: 204 (No Content); Time: 243ms; Content length: 0 bytes

### Updating Developer (Fail)

#### Request
PUT https://localhost:5001/api/Developers/3
Content-Type: application/json

{"developerId": 3, "name": "Adobe Inc.", "description": "Adobe Inc.", "birthDate": "2002-01-02", "profilePicture": "intellij.png", "phoneNumber": "9876 54 32 10", "email": "adobe_contact.com" }

/*Email doesn't have an @*/

#### Response
HTTP/1.1 400 Bad Request
Date: Sun, 19 Dec 2021 20:29:00 GMT
Content-Type: application/problem+json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-d78fef6c4d20624b8389892d80fba30c-86956b23cd986142-00",
  "errors": {
    "": [
      "Email must be valid! Use at least 1 '@' and 1 '.'."
    ]
  }
}

Response code: 400 (Bad Request); Time: 122ms; Content length: 260 bytes




















