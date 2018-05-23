Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        ViewData("Title") = "Home Page"

        Return View()
    End Function


    Function OurTeam() As ActionResult

        Dim countryState As Dictionary(Of String, KeyValuePair(Of String, List(Of String)))

        Dim states As New KeyValuePair(Of String, List(Of String))

        Return View()
    End Function

    function SomeMoreChanges() as ActionResult
        
    End  Function


End Class
