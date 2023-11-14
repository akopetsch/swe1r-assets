select Model as m, n, Unk
from Model_Vertex
inner join 
  (
    select Id as i, BlockItemValue.Name as n from BlockItemValue
    where BlockType = 0
    order by i asc
  ) cat
  on cat.i = Model
where Unk != 255
group by m, Unk
order by m;
