select Width_Unk / (CAST(Width as FLOAT)) as ratio_w from Model_MaterialTexture
group by ratio_w
order by ratio_w
-- 128.0
-- 256.0
-- 512.0

----------------------------------------------------------------------------------------------------

select Height_Unk / (CAST(Height as FLOAT)) as ratio_h from Model_MaterialTexture
group by ratio_h
order by ratio_h
-- 128.0
-- 256.0
-- 512.0

----------------------------------------------------------------------------------------------------

select Height_Unk from Model_MaterialTexture
group by Height_Unk
order by Height_Unk
-- 2048
-- 4096
-- 8192
-- 16384
-- 17920
-- 23552
-- 24064
-- 32768

----------------------------------------------------------------------------------------------------

select Width_Unk from Model_MaterialTexture
group by Width_Unk
order by Width_Unk
-- 2048
-- 4096
-- 8192
-- 16384
-- 32256
-- 32768
-- 37888
-- 39936
