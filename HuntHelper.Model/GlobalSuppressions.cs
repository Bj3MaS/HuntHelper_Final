
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

//No point Changing String url to Uri, using a clean string to Get images.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Scope = "member", Target = "HuntHelper.Model.Animal.#ImageUrl")]

//Naming
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "4#", Scope = "member", Target = "HuntHelper.Model.Animal.#.ctor(System.String,System.String,System.String,System.String,System.String,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", Scope = "type", Target = "HuntHelper.Model.ApiCall")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Scope = "member", Target = "HuntHelper.Model.HuntedAnimalPoints.#.ctor(System.String,HuntHelper.Model.Hunter,HuntHelper.Model.Animal,System.Double,System.Double,System.String,System.String,System.String)")]

