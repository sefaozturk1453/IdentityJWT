# IdentityJWT

Browser                                       Server
 |                                               |
 |  1.POST/user/login with username and password |
 |         ----------------->                    |  |
 |                                               |  |   2.Creates a JWT with a secret
 |                                               |  V
 |  3.Returns the JWT to the Browser             |
 |         <----------------                     |
 |  4.Sends the JWT on the Authorization Header  |
 |          ---------------->                    |  |
 |                                               |  |   5.Check JWT singnature. Get user information from the JWT
 |                                               |  V
 |  6.Sends response to the client               |
            <----------------
            
            
   .Net Core 2.1
   
