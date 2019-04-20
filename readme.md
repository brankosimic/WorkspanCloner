

<a id="org94c5fde"></a>

# .NET Core and Workspan Cloner

This is a console application written in .NET Core 2.1 that recieves a .json file containing entities and links between them.
The app also recieves the ID of the entity that needs to be cloned. 
Cloning process also clones all the entities that are related to that node whether they are direct descdenents or indirect descdendents.
The links are updated with new nodes and all the nodes that were pointing to original node also point to cloned node.
All nodes are connected with other nodes in Parents and Children relation.

<a id="org320aa52"></a>

# Step 1: Install .NET Core

First step is to [install .NET core](https://dotnet.microsoft.com/download/dotnet-core/2.1), this is pretty easy to do, just follow the instructions. You'll know that the install worked if you can type this in your terminal and see a result:

```sh
dotnet --version
```


<a id="orgd0bf219"></a>

# Step 2: Clone the repository

<a id="org9ac7c6b"></a>

```sh
git clone https://github.com/brankosimic/WorkspanCloner.git
cd WorkspanCloner

```


<a id="orgff68563"></a>

# Step 3: Run the example

Once you have cloned the repository, run the following command to "restore" the .NET dependencies listed in `project.json` file in both projects:

```sh
dotnet restore
```

Once that is complete, run the tests which will also build the project:

```
dotnet test
```

To use the application, run:

```
dotnet run --project WorkspanCloner WorkspanCloner/Resources/2levelexample.json 5
```

To use the application with your .json file, run:

```
dotnet run --project WorkspanCloner /Users/branko/Desktop/2levelexample.json 5
```

Note: Make sure that the file path is correct