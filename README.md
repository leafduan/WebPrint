WebPrint
======
* WebPrint is a simple order system for RFID web printing build with ASP.NET MVC, NHibernate, Autofac, log4net, etc.
* You can use this system to place order and print tags with bartender.
* And it's also an architectural foundation for building modern web applications.


### Features:
* Using ASP.NET MVC 4.
* Generic repository for NHibernate
* Repository Pattern and Unit of Work-Dependency Injection using Autofac
* ASP.NET MVC Area for specific fucntion pages
* Using NHibernate for mappings in strongly typed C# code
* Using NHibernate Linq for CRUD
* One session per request pattern using autofac
* Using log4net for logging
* Using NPOI dealing with office excel 97~3003
* Using bartender printing RFID tags
* Using NHibernate Profiler profiling sql statement

### Architectural & Design Patterns:
* Model-View-Controller with ASP.NET MVC
* Object-relational mapping with NHibernate
* Dependency Injection with Autofac
* Domain Driven Design
* Data,Repository,Service pattern with NHibernate
* Unit of work pattern
* And much more


--------------------------------------------------------------------------------------------------

### Version 1.0

### Technologies:
* ASP.NET MVC 4 
* ASP.NET Web API
* NHibernate 3.3.1.4000
* Autofac 3.1.5
* log4net 1.2.13.0
* NPOI 1.2.5
* Bartender 9.30.2.0(optional)
* Npgsql 2.1.3.0
* NHibernate Profiler(optional)(http://www.hibernatingrhinos.com/products/nhprof)
* MicroOLAP Database Designer for PostgreSQL(optional) 

### Requirements:
* Visual Studio 2010
* PostgreSQL 8.4.21 and up


--------------------------------------------------------------------------------------------------

### Notes
This project takes advantages of FNHMVC(https://github.com/LeafDuan/FNHMVC/) and Orchard(http://www.orchardproject.net/)

--------------------------------------------------------------------------------------------------
### Roadmap
* Improving DataBaseService to common
* NHibernate 4.0
* NOPI 2.0, support xlsx


