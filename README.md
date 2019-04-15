# SimpleObjectMapper
simple object mapper is a simple class that helps you to map any object to another object that has same or some of same properties 
- you can map entity => dto 
- list to list 
- update object with another object 
- clone an object 

# Examples 

-List<ProductDto> products = ObjectMapper.GetEntityList<ProductDto>(product);
  
-var item = ObjectMapper.GetEntity<Type>(anotherType);
  
-ObjectMapper.FillObject(source, target);
