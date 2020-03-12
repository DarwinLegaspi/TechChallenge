**__"NOMSS Customer Order Fulfilment TechChallenge"__** 

**Assumptions**
- An Order can contain multiple product orders,
  and if one of product order is unfulfillable, mark the order as unfulfillable.

- A purchase order will only be raised for a product id when an order can be fulfilled.   
  If an order is unfulfillable then no stock is taken from the product quantity on hand and no purchase order is made.  
  A purchase order most probably has been made before the product quantityOnHand has fallen below the reorderThreshold,
  such as in the case of Product ID: 3 in data.json.

- A purchase order for a product will only be raised once.

- An order Id will always be an integer

- All quantity are not UOM (Unit of measure) aware

- Used data.json as the sample data set.

**Validation**
- No validation done on a badly formatted input for the fulfilment endpoint.

- A Bad Request response is sent by the fulfillment endpoint on the following occasion:
  - An empty Order Ids is received by the endpoint.
  - When an order Id is not found in data.json
  
      
**Technical Stack**
- dotnet core 2.1 (C#)
- NancyFX
- AspNetCore.Server.Kestrel
- AspNetCore.Owin

**To run the API**
- make sure to install dotnet core 2.1 or higher
- and run dotnet run on the project level directory

**EndPoint routes available**
- Main Endpoint 
  - Post "/api/v1/warehouse/fulfilment"  

- Debug Endpoints
  - Get "/" 
    -  Returns a text "Welcome to NOMSS" to make sure the web api is working.

  - Get "/api/v1/warehouse/fulfilment/purchaseorders"
    -  Determine which product Ids has a purchase order. 
  
  - Get "/api/v1/warehouse/fulfilment/statussummary"
    -  Returns the status of all products (qty, etc)
    -  Returns the status of all orders (Fulfilled/Unfulfilled)

  - Post "/api/v1/warehouse/fulfilment/reset"
    -  Resets all states back to the startup values
    
  > sample usage:
    - issue a Post "/api/v1/warehouse/fulfilment"  
    - issue a Get "/api/v1/warehouse/fulfilment/purchaseorders" to get the purchase orders raised.
    - issue a Get "/api/v1/warehouse/fulfilment/statussummary" to get the current product quantities and order status.
    - issue Post "/api/v1/warehouse/fulfilment/reset" to bring back all test data
      to startup values.  Just like restarting the webapp. 

**sample structure**
- src/nancymodules/OrderFulfilmentModule.cs contains the endpoint controller
- sr/providers/OrderFulfilmentProvider.cs contains the main Order
  Fulfilment process.  Contains the business logic.

- sr/providers/*, contains all service provider needed specific to 
  Products, Orders, Purchase Orders.  

