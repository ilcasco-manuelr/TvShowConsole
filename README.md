# **TV SHOW Console.**

*Challenge App to handle some actions of TV Shows.*

## **How to execute?**

Once you download/fork/clone the project, navigate to TvShowConsole Folder and execute `dotnet run`, this commmand you can execute from CMD, Bash or PowerShell.

A database SQLite (tvshows.db) will create automatically if not exists on Data folder.

You will see a menu like this.

![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/e78c2b99-4592-4aa5-9ce4-2eba1b40b374)


now you can start to play with this.


### ** Note **

Use the below command in case that you will need to modify the model on db to create it

**Create a migration file to create the database model**
* [ ] `dotnet ef migrations add InitialCreate`

**update/create the database**
* [ ] `dotnet ef database update`



## **Unit Test**

Navigate under the TestTvShows folder and execute `dotnet test`, this commmand you can execute from CMD, Bash or PowerShell.

Test will appear like this

![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/2c1118bf-c705-494a-b65f-f980d82dd7da)



## **List of Actions**

1. **List all TV Shows from Database**
	Use command "`list`" to display all TV Shows existing on DB.

      A "*" will show on those TV Shows marked as favorite
   
      ![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/23f5241c-75b1-460d-a8d7-421f72cf53c5)

2. **Get information for specific TV Show**

   Use command "`info <Id>`# to display information about a TV Show

      Example:
      `info 1`

      ![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/ef7a56c6-5ae0-4589-8358-a0483b03c0bf)

3. **Add new TV Show from API to local DB**

   Use command "`add <Id>`" to add a new TV show from API

   Example
   `add 100`

   ![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/ed07e8b9-38f3-420a-85b4-10b73f83d2c6)

4. **Show All Favorite TV shows**

   Use command `favorites` to list all TV Shows marked as favorite

   Example
   `favorites`

   ![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/1bea8b62-41ca-48b9-adef-b65b91f244c2)

5. **Mark/Unmark as Favorite TV Show**

   Use command `mark <Id>` to mark/unmark a TV Show as favorite.

   Example:
        `mark 100`

   If a TV show is not marked as favorite this command will mark it as Favorite.

   ![image](https://github.com/ilcasco-manuelr/TvShowConsole/assets/125287482/0616815a-37b6-4373-90af-56213ae2944a)




   
