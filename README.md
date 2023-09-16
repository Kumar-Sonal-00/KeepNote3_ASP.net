## Seed code - Boilerplate for step 3 - Keep Note Assignment

### Assignment Step Description

In this Case study: Keep Note Step 3, we will create a RESTful application. 

Representational State Transfer (REST) is an architectural style that specifies constraints. 
In the REST architectural style, data and functionality are considered resources and are accessed using Uniform Resource Identifiers (URIs), typically links on the Web.

Resources are manipulated using a fixed set of four create, read, update, delete operations: PUT, GET, POST, and DELETE. 
 - POST creates a new resource, which can be then deleted by using DELETE. 
 - GET retrieves the current state of a resource in some representation. 
 - PUT transfers a new state onto a resource. 

### Problem Statement

In this case study, we will develop a RESTful application with which we will register a user, create a note and delete a note, add a note into the different category, set a reminder to the note. Also, we will perform authentication like login. Check the performance of the operations with the help of Postman API.

### Solution Step

        Step 1: Configure Postman in your Google Chrome
        Step 2: Use URI's mentioned in the controller to check all the expected operations using Postman.

### Following are the broad tasks:

 - Create a new user, update the user, retrieve a single user, delete the user.
 - Login using userId and password.
 - Create a Note, update a note,  delete a note, get all notes of a specific userId.
 - Create a Category, update a Category,  delete a Category, get all Categories of a specific userId.
 - Create a Reminder, update a Reminder,  delete a Reminder, get all Reminders of a specific userId.
 

### Steps to be followed:

    Step 1: Clone the boilerplate in a specific folder on your local machine.
    Step 2: Define the data model classes (User, Reminder, Note, Category)
    Step 3: See the methods mentioned in the Repository interface.
    Step 4: Implement all the methods of Repository interface.
    Step 5: Test each and every Repository with appropriate test cases.
    Step 6: See the methods mentioned in the service interface.
    Step 7: Implement all the methods of service interface.
    Step 8: Test each and every service with appropriate test cases.
    Step 9: Write controllers to work with RESTful web services. 
    Step 10: Test each and every controller with appropriate test cases.
    Step 11: Check all the functionalities using URI's mentioned in the controllers with the help of Postman for final output.

### Project structure

The folders and files you see in this repositories is how it is expected to be in projects, which are submitted for automated evaluation by Hobbes

    Solution
	|
	├──Keepnote
	|	└─Controllers
	|		└── NoteController.cs 		         // This class is used to control all the transactions for Note.
    |	    └── UserController.cs 		         // This class is used to control all the transactions for User.
    |		└── CategoryController.cs 		     // This class is used to control all the transactions for Category.
    |		└── ReminderController.cs 		     // This class is used to control all the transactions for Reminder.
    ├──Entities
	|		└── Note.cs                    	// This class will be acting as the data model for the Note data in the SQL Server. 
    |		└── User.cs                    	// This class will be acting as the data model for the User data in the SQL Server.
    |		└── Reminder.cs                  // This class will be acting as the data model for the Reminder data in the SQL Server.
    |		└── Category.cs                  // This class will be acting as the data model for the Category data in the SQL Server.
    ├─────DAL
    |       └── KeepDbContext.cs          // This class will be acting as DbContext to speak to database in EF Core Code-First. 
    |       └── NoteRepository.cs          // This class will be acting as Data Access layer for Note. 
    |       └── UserRepository.cs          // This class will be acting as Data Access layer for User. 
    |       └── CategoryRepository.cs      // This class will be acting as Data Access layer for Category. 
    |       └── ReminderRepository.cs     // This class will be acting as Data Access layer for Reminder. 
    |       └── INoteRepository.cs          // This interface contains all the behaviors of Note. 
    |       └── IUserRepository.cs          // This interface contains all the behaviors of User. 
    |       └── ICategoryRepository.cs      // This interface contains all the behaviors of Category. 
    |       └── IReminderRepository.cs      // This interface contains all the behaviors of Reminder. 
    ├─────Service
    |       └── NoteService.cs          // This class will be acting as Business layer for Note. 
    |       └── UserService.cs          // This class will be acting as Business layer for User. 
    |       └── CategoryService.cs      // This class will be acting as Business layer for Category. 
    |       └── ReminderService.cs     // This class will be acting as Business layer for Reminder. 
    |       └── INoteService.cs          // This Interface contains all business rules for Note. 
    |       └── IUserService.cs          // This Interface contains all business rules for User. 
    |       └── ICategoryService.cs      // This Interface contains all business rules for Category. 
    |       └── IReminderService.cs     // This Interface contains all business rules for Reminder.
    ├───── Exceptions
    |        └──CategoryNotFoundException.cs    // This class extends ApplicationException and used for a custom exception. 
    |        └──NoteNotFoundException.cs         // This class extends ApplicationException and used for a custom exception. 
    |        └──ReminderNotFoundException.cs     // This class extends ApplicationException and used for a custom exception. 
    |        └──UserAlreadyExistException.cs     // This class extends ApplicationException and used for a custom exception. 
    |        └──UserNotFoundException.cs         // This class extends ApplicationException and used for a custom exception. 
	|
	├──Test
	|	├──  Test.controller                  
    |   |        └── CategoryControllerTest.cs
    |   |        └── NoteControllerTest.cs
    |   |        └── ReminderControllerTest.cs
    |   |        └── UserControllerTest.cs
    |   ├── Test.Repository
    |   |        └── CategoryRepositoryTest.cs
    |   |        └── NoteRepositoryTest.cs
    |   |        └── ReminderRepositoryTest.cs
    |   |        └── UserRepositoryTest.cs
    |   ├── Test.service
    |           └── CategoryServiceTest.cs
    |           └── NoteServiceTest.cs
    |           └── ReminderServiceTest.cs
    |           └── UserServiceTest.cs
	|
	├── .hobbes              / Hobbes specific config options, such as type of evaluation schema, type of tech stack etc., Have saved a default |                               values for convenience
	├── .sln			                            // This is automatically generated by Visual Studio.
	└── README.md  		            // This files describes the problem of the assignment/project, you can provide as much as information and   |                                    clarification you want about the project in this file
    

#### To use this as a boilerplate for your new project, you can follow these steps

1. Clone the base boilerplate in the folder **assignment-solution-step3** of your local machine
     
    `git clone https://gitlab-cts.stackroute.in/stack_dotnet_keep/KeepNote-Step3-Boilerplate.git assignment-solution-step3`

2. Navigate to assignment-solution-step3 folder

    `cd assignment-solution-step3`

3. Remove its remote or original reference

     `git remote rm origin`

4. Create a new repo in gitlab named `assignment-solution-step3` as private repo

5. Add your new repository reference as remote

     `git remote add origin https://gitlab-cts.stackroute.in/{{yourUserName}}/assignment-solution-step3.git`

     **Note: {{yourUserName}} should be replaced by your userName from gitlab**

5. Check the status of your repo 
     
     `git status`

6. Use the following command to update the index using the current content found in the working tree, to prepare the content staged for the next commit.

     `git add .`
 
7. Commit and Push the project to git

     `git commit -a -m "Initial commit | or place your comments according to your need"`

     `git push -u origin master`

8. Check on the git repo online, if the files have been pushed

### Important instructions for Participants
> - We expect you to write the assignment on your own by following through the guidelines, learning plan, and the practice exercises
> - The code must not be plagirized, the mentors will randomly pick the submissions and may ask you to explain the solution
> - The code must be properly indented, code structure maintained as per the boilerplate and properly commented
> - Follow through the problem statement shared with you

### MENTORS TO BEGIN REVIEW YOUR WORK ONLY AFTER ->
> - You add the respective Mentor as a Reporter/Master into your Assignment Repository
> - You have checked your Assignment on the Automated Evaluation Tool - Hobbes (Check for necessary steps in your Boilerplate - README.md file. ) and got the required score - Check with your mentor about the Score you must achieve before it is accepted for Manual Submission.
> - Intimate your Mentor on Slack and/or Send an Email to learner.support@stackroute.in - with your Git URL - Once you done working and is ready for final submission.