## Initialise data:

**Method:**
GET

**URL:**
https://localhost:8080/initialisation

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "Initialised data",
    "contentList": null
}
```

# User accounts

## Customer account creation:

**Method:**
POST

**URL:**
https://localhost:8080/user/addcustomer

**Body:**
```
["{FirstName}","{LastName}","{Email}","{Password}"]
```

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "Customer account created successfully",
    "contentList": [
        "6dc49d56-5ec9-4c08-b8cb-bf37ea5a0f97"
    ]
}
```

#### :astonished:**Note:**
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

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "Admin account created successfully",
    "contentList": [
        "af0f965b-7cd3-45b6-ba8c-e9130ac87acd"
    ]
}
```

#### :astonished:**Note:**
Password must be 64 characters long. Intention is that this is the result of using SHA-256 on plain text password.

## Login:

**Method:**
POST

**URL:**
https://localhost:8080/user/login

**Body:**
```
["{Email}","{Password}"]
```

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "Login successful",
    "contentList": [
        "{AccessToken}"
    ]
}
```

#### :astonished:**Note:**
AccessToken will be a string representing an AccessToken created for this login session. It will be tied to the user that was logged in and will expire 5 minutes after it's last verification.

## Verify and refresh access token:

**Method:**
GET

**URL:**
https://localhost:8080/user/verify/{AccessToken}

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "Access token is valid and has been refreshed.",
    "contentList": [
        "{AccessToken}"
    ]
}
```

**:poop:Example negative response:**
```
{
    "successful": false,
    "body": "Unable to verify access token",
    "contentList": null
}
```

#### :astonished:**Note:**
AccessToken will be returned in the response but there is no need to use this as it will not have been changed. The access token will have it's lifetime refreshed by calling this API.

Whether the access token has expired or never existed, the response will be the same.

## Logout:

**Method:**
POST

**URL:**
https://localhost:8080/user/logout/{AccessToken}

**:+1: Example positive response:**
```
{
    "successful": true,
    "body": "User is now logged out",
    "contentList": null
}
```

# Films

## Get new films:

**Method:**
GET

**URL:**
https://localhost:8080/film/getnewfilms

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved all newly released films",
    "contentList": [{Film1},{Film2},{Film3}]
}
```

#### :astonished:**Note:**
Each film will be a json object of the following format:

```
{
    "id": "{Unique Id of film}",
    "title": "{Title of film}",
    "isReleased": {Boolean representing whether the film has been released. Will be true for new films, false for upcoming},
    "length": "{Runtime of film}",
    "briefDescription": "{Brief text describing film}",
    "detailedDescription": "{More details synopsis describing film}",
    "imagePath": "{Path of image to display}",
    "year": "{Year of release of film}",
    "classification": "{Classification of film (e.g. 12A)}",
    "directors": ["{Director of film}","{Another director of film}"],
    "genres": ["{Genre of film}","{Another genre film may fit into}"],
    "actors": ["{Lead actor/actress}","{another lead actor/actress}"],
    "javaClass": "{String for backwards compatibility with spring boot back end, usually left as null}"
}
```

## Get upcoming films:

**Method:**
GET

**URL:**
https://localhost:8080/film/getupcomingfilms

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved all soon to be released films",
    "contentList": [{Film1},{Film2},{Film3}]
}
```

## Get specific film:

**Method:**
GET

**URL:**
https://localhost:8080/film/getfilm/{FilmId}

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Film data retrieved",
    "contentList": [{Film}]
}
```

## Perform a search for film(s):

**Method:**
GET

**URL:**
https://localhost:8080/film/search/{SearchQuery}

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Search complete. Found 3 films",
    "contentList": [{Film1},{Film2},{Film3}]
}
```

#### :astonished:**Note:**
Search will be performed based on a match with title, genres, actors, directors. They will not be sorted, leaving them in the order they were added to the database.

## Add film:

**Method:**
POST

**URL:**
https://localhost:8080/film/add

**Body:**
```
{
    "title": "{Title of film}",
    "isReleased": {Boolean representing whether the film has been released. Will be true for newly released films, false for upcoming},
    "length": "{Runtime of film}",
    "briefDescription": "{Brief text describing film}",
    "detailedDescription": "{More details synopsis describing film}",
    "imagePath": "{Path of image to display}",
    "year": "{Year of release of film}",
    "classification": "{Classification of film (e.g. 12A)}",
    "directors": ["{Director of film}","{Another director of film}"],
    "genres": ["{Genre of film}","{Another genre film may fit into}"],
    "actors": ["{Lead actor/actress}","{another lead actor/actress}"],
    "javaClass": null
}
```

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Film successfully added to database.",
    "contentList": [{Film1},{Film2},{Film3}...]
}
```

#### :astonished:**Note:**
contentList will contain a list of all films in the database.

## Update film:

**Method:**
POST

**URL:**
https://localhost:8080/film/update

**Body:**
```
{
    "id": "{Unique Id of film, must match Id of the film to be updated}"
    "title": "{Title of film}",
    "isReleased": {Boolean representing whether the film has been released. Will be true for newly released films, false for upcoming},
    "length": "{Runtime of film}",
    "briefDescription": "{Brief text describing film}",
    "detailedDescription": "{More details synopsis describing film}",
    "imagePath": "{Path of image to display}",
    "year": "{Year of release of film}",
    "classification": "{Classification of film (e.g. 12A)}",
    "directors": ["{Director of film}","{Another director of film}"],
    "genres": ["{Genre of film}","{Another genre film may fit into}"],
    "actors": ["{Lead actor/actress}","{another lead actor/actress}"],
    "javaClass": null
}
```

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Film updated",
    "contentList": [{Film1},{Film2},{Film3}...]
}
```

#### :astonished:**Note:**
contentList will contain a list of all films in the database.

## Delete film:

**Method:**
GET

**URL:**
https://localhost:8080/film/delete/{FilmId}

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Film deleted",
    "contentList": [{Film1},{Film2},{Film3}...]
}
```

#### :astonished:**Note:**
contentList will contain a list of all films in the database.

# Reviews

## Get all reviews for a particular film:

**Method:**
GET

**URL:**
https://localhost:8080/reviews/getreviews/{FilmId}

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved film data and reviews as a list. There was 4 reviews for that film",
    "contentList": [{Film},{Review1},{Review2},{Review3},{Review4}]
}
```

#### :astonished:**Note:**
Element 0 of contentList will be the film for which Id was provided. All subsequent elements will be the reviews for that film if there are any.

## Add a review:

**Method:**
POST

**URL:**
https://localhost:8080/reviews/addreview

**Body:**
```
{
    "filmId":"{filmId which the review is related to}",
    "username":"{Username to be displayed as the author of the review}",
    "rating":"{A number to signify the user's rating, still formatted as a string}",
    "reviewBody":"{The review itself}"
}
```

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved film data and reviews as a list. There was 5 reviews for that film",
    "contentList": [{Film},{Review1},{Review2},{Review3},{Review4},{Review5}]
}
```

#### :astonished:**Note:**
Element 0 of contentList will be the film for which Id was provided. All subsequent elements will be the reviews for that film, including the one just added.

# Comments

## Get comments relating to a review:

**Method:**
GET

**URL:**
https://localhost:8080/comments/getcomments/{reviewId}

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved 2 comments for that review.",
    "contentList": [{Film},{Review},{Comment1},{Comment2}]
}
```

#### :astonished:**Note:**
First element of contentList will be the film, second element will be the review, subsequent elements will be the comments for that review if there are any.

## Add a comment:

**Method:**
POST

**URL:**
https://localhost:8080/comments/addcomment

**Body:**
```
{
    "reviewId":"{Unique Id of review}",
    "username":"{Username of author}",
    "body":"{Body of the comment}"
}
```

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved 2 comments for that review.",
    "contentList": [{Film},{Review},{Comment1},{Comment2}]
}
```

#### :astonished:**Note:**
First element of contentList will be the film, second element will be the review, subsequent elements will be the comments for that review, including the one just added.

# Showings

## Get all showings for a film:

**URL:**
https://localhost:8080/showings/getshowings/{filmId}

**Method:**
GET

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Retrieved 8 showings for that filmId",
    "contentList": [{Film},{Showing1},{Showing2}]
}
```

#### :astonished:**Note:**
Showing object will be of the following form:

```
{
    "id": "{Unique Id of the showing}",
    "filmId": "{Unique Id of the film being shown}",
    "showingTime": "{string representation of the time of the screening}",
    "date": "{string representation of the date of the screening}",
    "seatsAvailable": {int representing the number of seats still available in the screening},
    "totalNumberOfSeats": {int representing the total number of seats available in the screening},
    "seatAvailability": {2 dimensional array of booleans, each value representing whether a seat is taken. False means the seat is not taken},
    "screenType": "Standard",
    "pricePerSeat": 8,
    "javaClass": null
}
```

# Bookings

## Book some seats for a showing:

**URL:**
https://localhost:8080/booking/booktickets/{showingId}

**Method:**
POST

**Body:**
```
["{Seat to book e.g. 2:4 where 2 is the row number and 4 is the seat within that row}","2:5","2:6","2:7","2:8"]
```

**:+1:Example positive response:**
```
{
    "successful": true,
    "body": "Your seats have been booked",
    "contentList": null
}
```

**:-1:Example negative response:**
```
{
    "successful": false,
    "body": "Unable to proceed with booking. Some of those seats have already been booked.",
    "contentList": [{Updated showing information}]
}
```
