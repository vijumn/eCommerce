# eCommerce
The eCommerce application using Microservice achiectural style.
Two Microservices
1. Catalog
  a.Catalog serves available products and promotions   
3. Trolley
  a. Trolley is used to to store and calculate user shopping trolley   


Running the Application
  Use the docker compose up to start the application.
  Catalog API : http://localhost:5800/api/v1
  Trolley API :http://localhost:5801/api/v1

PostMan Collection : https://www.postman.com/vijumn/workspace/e-commerce/collection/2320746-0aca2e52-7d44-4b00-9e6b-ffd9cb28ade4?action=share&creator=2320746




TODO:
1.Refactor Promotion table to reduce number of columns used to store the  discount information
2.Add Date time  to get the only active promotions at a give point of time.
3.Unit test cases for Catalog API
4.Add Application monitoring and insights
5.Implement Trolley storage and retrieve from Redis
6.Add UI
7. Add validate productid's when trolley receives unknon products


   
