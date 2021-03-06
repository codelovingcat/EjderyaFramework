﻿using EjderyaFramework.Business.ValidationRules.FluentValidation;
using EjderyaFramework.Core.Aspects.Pastsharp;
using EjderyaFramework.Core.Aspects.Pastsharp.CacheAspects;
using EjderyaFramework.Core.Aspects.Pastsharp.ExceptionAspects;
using EjderyaFramework.Core.Aspects.Pastsharp.LogAspects;
using EjderyaFramework.Core.Aspects.Pastsharp.PerformanceAspects;
using EjderyaFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using EjderyaFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EjderyaFramework.Business")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("EjderyaFramework.Business")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: LogAspect(typeof(FileLogger), AttributeTargetTypes = "EjderyaFramework.Business.Concrete.Manager.*")]
[assembly: LogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "EjderyaFramework.Business.Concrete.Manager.*")]
[assembly: ExceptionLogAspect(typeof(DatabaseLogger), AttributeTargetTypes = "EjderyaFramework.Business.Concrete.Managers.*")]
[assembly: PerformanceCounterAspect(AttributeTargetTypes = "EjderyaFramework.Business.Concrete.Managers.*")]
// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3e0979e1-c1a4-4bd9-a984-407d079fd7f8")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
