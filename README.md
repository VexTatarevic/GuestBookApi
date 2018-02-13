# GuestBookApi
Serverside REST Api in .Net Core C# for the GuestBook App
This is a REST Api service implementation with .Net Core Web Api and Entity Framework.

This Web Api will be consumed in future by a client side app built in Angular 5.

Some of the features demonstrated in this project are:

.Net Logging using .Net Core's out of box ILogger

Automatic Model Mapping - Using AutoMapper to map Entity objects to dtos (Data Transfer Objects) :

var dto = _mapper.Map<Message, MessageDto>(entity);

Web Api - using full set of actions to query database : GET, POST, PUT, DELETE

Web Api Named routes to better structure return urls :

[HttpGet("{id:int}", Name = "MessageGet")] public IActionResult Get(int id) .... [HttpPost] public async Task<IActionResult> Post([FromBody]MessageDto dto) { ... var newMessageUri = Url.Link("MessageGet", new { id = messageDto.Id }); return Created( newMessageUri, messageDto);

Web Api Associative Controller feature eg. /api/guestbook/category/2/messages

Asynchrounous Api and data requests using c# Async Await to improve responsiveness

Repository data access design pattern

Serverside Paging using linq Skip and Take

Dynamic search query building using Expression predicates :

public IEnumerable<Message> GetPageOfMessages(MessageSearchDto s, Expression<Func<Message, bool>> predicate)

Serverside Sorting implemented using c# extension methods inside Web.Data.ExtIQueryable

Entity Framework - Custom table naming and modularization of tables by using sql server schemas

To test the project I recommend using Postman Http Request and Response tool which you can download from: https://www.getpostman.com/

You can test functionality of the api by calling the following Urls and associated REST actions:

REST API:
Root Url is:
http://www.vexit.com/ or vexit.com/

Append the following api routes to Root Url in order to test the api e.g. vexit.com/api/guestbook/messages

Messages
Action	Url	Description
POST	/api/guestbook/messages	create new message
PUT	/api/guestbook/messages/5	update message with id=5
DELETE	/api/guestbook/messages/3	delete message with id=3
GET	/api/guestbook/messages/1	get details for message with id=1
GET	/api/guestbook/messages	default page 1, size 20 items per page, no search criteria
GET	/api/guestbook/messages?page=3&size=2	page 3, 2 items per page, no criteria
GET	/api/guestbook/messages?read=true	page 1, mesages that have been read
GET	/api/guestbook/messages?cat=4	messages in category 4
GET	/api/guestbook/messages?sender=John	where sender name contains the word "John"
GET	/api/guestbook/messages?email=tom@gmail.com	where sender email is tom@gmail.com
GET	/api/guestbook/messages?subject=question	where subject contains the word "question"
GET	/api/guestbook/messages?from=2018-02-12	where message sent on or after date 2018-02-12
GET	/api/guestbook/messages?from=2018-02-11&to=2018-02-12	sent from date 2018-02-11 to date 2018-02-12 inclusive
GET	/api/guestbook/messages?sortby=DateSent&desc=true	orderby DateSent descending
You can use any combination of the search fields in url for GET. The following is the list of all the search fields for Messages:

Field	Description
page	Current page number
size	Number of records per page
sortby	Field by which the records are ordered
desc	True means Sort in Descending order. False : sort in Ascending order
cat	Message Category Id
sender	Part of or whole sender's name
email	Sender Email
subject	Message Subject
read	Boolean - Has message been read. read=true returns all messages that have been read
from	Date - return messages sent from and including this date
to	Date - return messages sent upto and including this date

Categories
Action	Url	Description
GET:	/api/guestbook/categories	Get all Categories
GET:	/api/guestbook/categories/1	Get category with id = 1

CategoryMessages
Action	Url	Description
GET:	/api/guestbook/category/1	get all messages in category 1
GET:	/api/guestbook/category/1/messages/4	get message 4 in category 1
