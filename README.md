"# TechChallenge" 

       validate Orders
           validate Orders should validate if product in order still exists
           will need to return a validation exception if an order is not valid 
           or a product order is not valid.

      
        if an order does not exist
          respond with bad request
        if a product does not exist  
          respond with bad request

          major Assumption
             An Order can contain multiple product orders,
             and if one of product order is unfulfillable, mark the order as unfulfillable
          
          An order run is the list of input orderIds