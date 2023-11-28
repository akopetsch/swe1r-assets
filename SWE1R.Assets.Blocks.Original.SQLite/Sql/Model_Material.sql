select

  -- # Model # --
  BlockItemValue.Id as mod_id,
  BlockItemValue.Name as mod_name,

  -- # Material # --
  printf('0x%06x', Model_Material.Offset) as m_off_x,
  Model_Material.Int as m_i,
  Model_Material.Width_Unk_Dividend as m_w_ud,
  Model_Material.Height_Unk_Dividend as m_h_ud,
  printf('0x%06x', Model_Material.P_Texture) as m_p_t_x,
  printf('0x%06x', Model_Material.P_Properties) as m_p_p_x,
  
  -- # Model_MaterialTexture # ---
  Model_MaterialTexture.Mask_Unk as mt_m_u,
  -- Model_MaterialTexture.Width4 as mt_w4,
  -- Model_MaterialTexture.Height4 as mt_h4,
  -- Model_MaterialTexture.Always0_08 as mt_08,
  -- Model_MaterialTexture.Always0_0a as mt_0a,
  Model_MaterialTexture.Byte_0c as mt_b_c,
  Model_MaterialTexture.Byte_0d as mt_b_d,
  Model_MaterialTexture.Word_0e as mt_w_e,
  Model_MaterialTexture.Width as mt_w,
  Model_MaterialTexture.Height as mt_h,
  Model_MaterialTexture.Width_Unk as mt_w_u,
  Model_MaterialTexture.Height_Unk as mt_h_u,
  Model_MaterialTexture.Flags as mt_f,
  printf('0x%04x', Model_MaterialTexture.Mask) as mt_m,
  printf('0x%06x', Model_MaterialTexture.P_Child0) as mt_p_c0,
  printf('0x%06x', Model_MaterialTexture.P_Child1) as mt_p_c1,
  printf('0x%06x', Model_MaterialTexture.P_Child2) as mt_p_c2,
  printf('0x%06x', Model_MaterialTexture.P_Child3) as mt_p_c3,
  printf('0x%06x', Model_MaterialTexture.P_Child4) as mt_p_c4,
  printf('0x%06x', Model_MaterialTexture.IdField) as mt_id_x,
    ((CASE WHEN P_Child0 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child1 != 0 THEN 1 ELSE 0 END) +
	   (CASE WHEN P_Child2 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child3 != 0 THEN 1 ELSE 0 END) +
     (CASE WHEN P_Child4 != 0 THEN 1 ELSE 0 END)) AS childrenNonNullCount,
  (Model_MaterialTexture.IdField & 0xFFFFFF) as mt_id_m,
  
  -- # Model_MaterialTextureChild # --
  Model_MaterialTextureChild.Byte_1 as mtc0_b1,
  Model_MaterialTextureChild.Byte_2 as mtc0_b2,
  printf('0x%02x', Model_MaterialTextureChild.Byte_3) as mtc0_dims,
  Model_MaterialTextureChild.Byte_4 as mtc0_b4,
  Model_MaterialTextureChild.Byte_5 as mtc0_b5,
  Model_MaterialTextureChild.Byte_6 as mtc0_b6,
  Model_MaterialTextureChild.Byte_7 as mtc0_b7,
  Model_MaterialTextureChild.Byte_c as mtc0_bc,
  Model_MaterialTextureChild.Byte_d as mtc0_bd,
  Model_MaterialTextureChild.Byte_e as mtc0_be,
  Model_MaterialTextureChild.Byte_f as mtc0_bf,
  
  -- # Model_MaterialProperties # --
  Model_MaterialProperties.AlphaBpp as mtp_abpp,
  Model_MaterialProperties.Word_4 as mtp_w4,
  printf('0x%08x', Model_MaterialProperties.Ints_6_0) as mtp_i60,
  printf('0x%08x', Model_MaterialProperties.Ints_6_1) as mtp_i61,
  printf('0x%08x', Model_MaterialProperties.Ints_e_0) as mtp_ie0,
  printf('0x%08x', Model_MaterialProperties.Ints_e_1) as mtp_ie1,
  -- Model_MaterialProperties.Unk_16 as mtp_u16,
  Model_MaterialProperties.Bitmask1 as mtp_b1,
  Model_MaterialProperties.Bitmask2 as mtp_b2,
  -- Model_MaterialProperties.Unk_20 as mtp_u20,
  Model_MaterialProperties.Byte_22 as mtp_b22,
  Model_MaterialProperties.Byte_23 as mtp_b23,
  Model_MaterialProperties.Byte_24 as mtp_b24,
  Model_MaterialProperties.Byte_25 as mtp_b25,
  -- Model_MaterialProperties.Unk_26 as mtp_u26,
  -- Model_MaterialProperties.Unk_28 as mtp_u28,
  -- Model_MaterialProperties.Unk_2a as mtp_u2a,
  -- Model_MaterialProperties.Unk_2c as mtp_u2c,
  Model_MaterialProperties.Byte_2e as mtp_b2e,
  Model_MaterialProperties.Byte_2f as mtp_b2f,
  Model_MaterialProperties.Byte_30 as mtp_b30,
  Model_MaterialProperties.Byte_31 as mtp_b31
  -- Model_MaterialProperties.Unk_32 as mtp_u32
  
from Model_Material
LEFT JOIN BlockItemValue on
  BlockItemValue.Id = Model_Material.BlockItem AND 
  BlockItemValue.BlockType = 0
LEFT JOIN Model_MaterialTexture on
  Model_MaterialTexture.Offset = Model_Material.P_Texture AND 
  Model_MaterialTexture.BlockItem = Model_Material.BlockItem
LEFT JOIN Model_MaterialTextureChild on 
  Model_MaterialTextureChild.Offset = Model_MaterialTexture.P_Child0 AND 
  Model_MaterialTextureChild.BlockItem = Model_Material.BlockItem
LEFT JOIN Model_MaterialProperties on 
  Model_MaterialProperties.offset = Model_Material.P_Properties AND 
  Model_MaterialProperties.BlockItem = Model_Material.BlockItem
where
  mt_id_m = 49 or
  mt_id_m = 58 or
  mt_id_m = 99 or
  mt_id_m = 924 or
  mt_id_m = 966 or
  mt_id_m = 972 or
  mt_id_m = 991 or
  mt_id_m = 992 or
  mt_id_m = 1000 or
  mt_id_m = 1048 or
  mt_id_m = 1064

  -- special1:
  -- mtc0_bd = 124 and 
  -- (mt_m = '0x01ff') and
  -- mt_f = 1024 and
  -- mtp_abpp = 0 and
  -- childrenNonNullCount = 1 and
  -- mt_m_u = 1 and
  -- mt_w_e = 0

  -- special2:
  mtc0_bd = 124 and
  (mt_m = '0xffffffffffffd78f')
