"# TechChallenge" 

       validate Orders
           validate Orders should validate if product in order still exists
           will need to return a validation exception if an order is not valid 
           or a product order is not valid.

      
        if an order does not exist
          respond with bad request
        if a product does not exist  
          respond with bad request

        Assumptions
             An Order can contain multiple product orders,
             and if one of product order is unfulfillable, mark the order as unfulfillable
          
             An order run is the list of input orderIds

             When an order can not be fulfilled and the product quantity on hand is 
             already below the reorder threshold, then a purchase order will not be raised.
             This is because no stock was taken from the product quantity on hand.


