select Width4 / (CAST(Width as FLOAT)) as ratio from Model_MaterialTexture
group by ratio
order by ratio
-- 4.0

----------------------------------------------------------------------------------------------------

select Height4 / (CAST(Height as FLOAT)) as ratio from Model_MaterialTexture
group by ratio
order by ratio
-- 4.0

----------------------------------------------------------------------------------------------------

select 
  printf("%s/%s", Width, Height) as ratio_i, 
  Width / (CAST(Height as FLOAT)) as ratio,
  Height / (CAST(Width as FLOAT)) as ratio_inv
from Model_MaterialTexture
group by ratio
order by ratio
-- 16/128	0.125	8.0
-- 32/128	0.25	4.0
-- 32/64	0.5	2.0
-- 32/46	0.695652173913043	1.4375
-- 64/64	1.0	1.0
-- 74/47	1.57446808510638	0.635135135135135
-- 63/32	1.96875	0.507936507936508
-- 64/32	2.0	0.5
-- 78/35	2.22857142857143	0.448717948717949
-- 64/16	4.0	0.25
-- 128/16	8.0	0.125
