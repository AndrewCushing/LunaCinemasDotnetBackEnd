# LunaCinemasDotnetBackEnd

## Customer account creation:

**Method:**
POST

**URL:**
https://localhost:8080/user/addcustomer

**Body:**
```
["{FirstName}","{LastName}","{Email}","{Password}"]
```

**Example positive response:**
```
{
    "successful": true,
    "body": "Customer account created successfully",
    "contentList": [
        "6dc49d56-5ec9-4c08-b8cb-bf37ea5a0f97"
    ]
}
```

#### **Note:**
Password must be 64 characters long. Intention is that this is the result of using SHA-256 on plain text password.

## Admin account creation:

**Method:**
POST

**URL:**
https://localhost:8080/user/addstaff

**Body:**
```
["{FirstName}","{LastName}","{Email}","{Password}"]
```

**Example positive response:**
```
{
    "successful": true,
    "body": "Admin account created successfully",
    "contentList": [
        "af0f965b-7cd3-45b6-ba8c-e9130ac87acd"
    ]
}
```

#### **Note:**
Password must be 64 characters long. Intention is that this is the result of using SHA-256 on plain text password.

## Initialise data:

**Method:**
GET

**URL:**
https://localhost:8080/initialisation

**Example positive response:**
```
{
    "successful": true,
    "body": "Initialised data",
    "contentList": null
}
```

## Login:

**Method:**
POST

**URL:**
https://localhost:8080/user/login

**Body:**
```
["{Email}","{Password}"]
```

**Example positive response:**
```
{
    "successful": true,
    "body": "Login successful",
    "contentList": [
        "{AccessToken}"
    ]
}
```

#### **Note:**
AccessToken will be a string representing an AccessToken created for this login session. It will be tied to the user that was logged in and will expire 5 minutes after it's last verification.

## Verify access token:

**Method:**
GET

**URL:**
https://localhost:8080/user/verify/{AccessToken}

**Example positive response:**
```
{
    "successful": true,
    "body": "Access token is valid and has been refreshed.",
    "contentList": [
        "{AccessToken}"
    ]
}
```

**Example negative response:**
```
{
    "successful": false,
    "body": "Unable to verify access token",
    "contentList": null
}
```

#### **Note:**
AccessToken will be returned in the response but there is no need to use this as it will not have been changed. The access token will have it's lifetime refreshed by calling this API.
