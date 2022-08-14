# _Factory Machine Management_

#### By _**Patty Otero**_

#### _A practice website for C# factory management-- this application allows factory management to manage a database of machines and engineers._

## Technologies Used

* _C#_
* _.NET 5.0_
* _ASP.NET Core_ 
* _CSS_
* _HTML_
* _Entity_
* _MySQL Workbench_
* _LINQ_

## Description

_A c# website where you can manage engineers and machines for a factory. You can track engineers and their machine specialties, and vice-versa.  The database utilizes a many-to-many model._

## Setup/Installation Requirements

* Clone this repository to your desktop
* Open your terminal and navigate to the top of this directory
* create a file called appsettings.json within the main project folder
* add the following text to the file inserting your own DATABASE NAME, USER ID, and PASSWORD: {
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE NAME HERE];uid=[USER ID HERE];pwd=[PASSWORD HERE];"
  }
}
* Navigate to ~/Factory in your terminal.
* Run the following commands:
>dotnet ef database update
>dotnet build
>dotnet run
* Use the localhost url with your web-browser to view the site

## Known Bugs

* _none_

## License

_MIT_

Copyright (c) _2022_ _Patty Otero_