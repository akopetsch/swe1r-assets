select Model as m, n, C_A
from Model_Vertex
inner join 
  (
    select Id as i, BlockItemValue.Name as n from BlockItemValue
    where BlockType = 0
    order by i asc
  ) cat
  on cat.i = Model
where C_A < 255
group by m, C_A
order by m;

----------------------------------------------------------------------------------------------------

-- ======= UNK < 255 ======= --

-- Unk from 0 to 254 means color
select * from Model_Vertex
where C_A >= 0 and C_A < 255
-- 11996 rows

-- R/G/B are 255 then
select * from Model_Vertex
where C_A >= 0 and C_A < 255
  and (C_R != 255 and C_G != 255 and C_B != 255)
-- none

-- most have Alpha > 0
select * from Model_Vertex
where C_A > 0 and C_A < 255
-- ------ 11996 rows ------ --
-- 147  Part_Explosion
-- ...  Part_TrackGround_*

-- few have Alpha = 0
select * from Model_Vertex
where C_A = 0
-- ------ 749 rows ------ --
-- 314 Modl_314
-- 313 Part_313
-- 147 Part_Explosion
-- 9   Podd_Aldar_Beedo
-- 2   Podd_Anakin_Skywalker
-- 14  Podd_Ark_Roose
-- 40  Podd_Ben_Quadinaros
-- 34  Podd_Boles_Roor
-- 32  Podd_Bozzie_Baranta
-- 26  Podd_Clegg_Holdfast
-- 301 Podd_Cy_Yunga
-- 22  Podd_Dud_Bolt
-- 20  Podd_Ebe_E_Endocott
-- 28  Podd_Elan_Mak
-- 38  Podd_Fud_Sang
-- 24  Podd_Gasgano
-- 299 Podd_Jinn_Reeso
-- 16  Podd_Mars_Guo
-- 12  Podd_Mawhonic
-- 46  Podd_Navior
-- 30  Podd_Neva_Kee
-- 36  Podd_Ody_Mandrell
-- 8   Podd_Ratts_Tyerell
-- 6   Podd_Sebulba
-- 42  Podd_Slide_Paramita
-- 4   Podd_Teemto_Pagalies
-- 44  Podd_Toy_Dampner
-- 17  Podd_Wan_Sandage

-- ======= UNK 255 ======= --

-- Unk = 255 indicates normal 

select * from Model_Vertex
where C_A = 255
-- 589212 rows

-- X/Y/Z bytes between 0 and 255, actually signed bytes with according range

-- normal can have a zero magnitude:
select * from Model_Vertex
where C_A = 255
  and (C_R = 0 and C_G = 0 and C_B = 0)
-- 11384 rows

-- normal can be (-1, -1, -1)
select * from Model_Vertex
where C_A = 255
  and (C_R = 255 and C_G = 255 and C_B = 255)
-- 39201 rows
