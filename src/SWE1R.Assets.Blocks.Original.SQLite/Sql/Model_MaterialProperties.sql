select 
  Model_Material.Model as m, 
  n, 
  printf('0x%x', Model_Material.Offset) as offset, 
  printf('0x%x', Model_Mesh.Offset) as 
  mesh_offset, 
  Model_Material.Int as int
from Model_Material
inner join 
  (
    select Id as i, BlockItemValue.Name as n from BlockItemValue
    where BlockType = 0
    order by i asc
  ) cat
  on cat.i = m
inner join Model_Mesh on Model_Mesh.P_Material = Model_Material.Offset
-- group by m, Model_Material.Int
where m = 115 and int = 6
order by m;
