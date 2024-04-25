namespace ShowAndTell.Components.Controllers

open System.Diagnostics

open Microsoft.AspNetCore.Mvc

open ShowAndTell.Components.Models

type HomeController () =
    inherit Controller()

    member this.Index () = this.View()

    member this.Partials () = this.View()
    member this.ViewComponents () = this.View()

    member this.JustPartial () =
        this.PartialView("Partials/_DeprecationWarningPartial")

    [<ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)>]
    member this.Error () =
        let reqId = 
            if isNull Activity.Current then
                this.HttpContext.TraceIdentifier
            else
                Activity.Current.Id

        this.View({ RequestId = reqId })
