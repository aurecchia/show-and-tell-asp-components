namespace ShowAndTell.Components.ViewComponents

open System
open Microsoft.AspNetCore.Mvc
open System.Threading.Tasks
open Microsoft.FSharp.Linq

type Item = {
    Name: string
    Price: decimal
}

type Cart = {
    Items: Item list
}

type CartsRepository() =
    member _.Load(id: int) =
        { Items = [
            { Name = "Item 1"; Price = 10m }
            { Name = "Item 2"; Price = 5m }
            { Name = "Item 3"; Price = 3.5m }
        ] }

[<ViewComponent>]
type CartViewComponent(cartsRepository: CartsRepository) =
    inherit ViewComponent()

    member this.InvokeAsync(cartId: int, [<OptionalArgument>] compact: bool) : Task<IViewComponentResult> =
        task {
            let cart = cartsRepository.Load(cartId)

            if compact then
                return this.View("CompactCart", cart)
            else
                return this.View(cart)
        }
