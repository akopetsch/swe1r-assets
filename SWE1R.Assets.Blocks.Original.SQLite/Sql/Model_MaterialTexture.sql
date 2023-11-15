select  
  name,
  printf('0x%x', Offset) as offset,
  printf('0x%x', IdField) as tIdFull,
  (IdField & 0xFFFFFF) as tId,
  *
from Model_MaterialTexture
inner join 
  (
    select Id as i, BlockItemValue.Name as name from BlockItemValue
    where BlockType = 0
    order by i asc
  ) cat
  on cat.i = Model
where 
  (Byte_0c = 2 and Byte_0d = 1) and
  (Width = 64 or Height = 64)
order by name;


select * from BlockItemValue
where BlockType = 3 and (Id = 64 or Id = 1185)
