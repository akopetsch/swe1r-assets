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

----------------------------------------------------------------------------------------------------

select P_Material, count(*) from Model_Mesh
where Model = 115
group by P_Material

SELECT count(*) as cnt,
    ((CASE WHEN P_Child0 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child1 != 0 THEN 1 ELSE 0 END) +
	   (CASE WHEN P_Child2 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child3 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child4 != 0 THEN 1 ELSE 0 END)) AS childrenNonNullCount
FROM Model_MaterialTexture
group by childrenNonNullCount
order by childrenNonNullCount
-- 17	  0
-- 4488	1
-- 4	  2
-- 26	  5

----------------------------------------------------------------------------------------------------
