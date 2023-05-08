# ObjectsHierarchyCreator


A simple C# .NET 5 WebApi simple backend service that gets a list of objects with id, name and parent and returns back the object heirarchal structure as a list of ancestors.



## How to run:
- Clone the repo
- Open cmd and ```cd``` into the repo's folder
- In the cmd, run ```dotnet build``` and wait for it to finish
- Then, run ```cd ObjectsHierarchyCreator.PL```
- run ```dotnet run```
- The port will be displayed in the cmd logs. On default it is 5000
- Open the browser and go to ```http://localhost:<port>/swagger```

## JWT secret key
For the authentication functionality, you can set you own secret key - open the ```appsettings.json``` in ```ObjectsHierarchyCreator.PL``` and set the JWTKey as needed.

## Auth
In the swagger, first use the ```api/auth/token``` method. The body is an object with 'username' and 'password'. For this simple purpose you can use 'admin' and '123'.
You can add more ```User``` objets in the file ```AccessControlRepository.cs``` file under ```ObjectsHierarchyCreator.DAL```.
Then, take the token from the response and click on ```Authorize``` button in the top right corner in swagger, and enter 'Bearer <TOKEN>' in the value field.

## Get Objects Hierarchy
In the swagger, use the ```api/ObjectsHierarchyCreator``` post method. See the ```exampleinput.txt``` and ```exampleoutput.txt```.

- ```id``` is the id of the object. It needs to be ```integer``` and the value needs to be at least 1.
- ```name``` is the name of the object, a ```string```
- ```parent``` is the id of the object's parent. if it has no parent, it can be either ```null``` or 0.
  
- No duplicates id's are alowed.
- If object has parent id X, an object with id X needs to be exist in the least.
