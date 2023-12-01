// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.TestUtils;
using SWE1R.Assets.Blocks.TestUtils;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.SQLite.Tests
{
    [Collection(NonParallelCollectionDefinition.Name)]
    public class ModelBlockItemsDeserializationSqlTest : 
        BlockItemsTestBase<ModelBlockItem>,  
        IClassFixture<AssetsDbContextFixture>, 
        IClassFixture<OriginalBlocksProviderFixture>
    {
        #region Fields

        private readonly AssetsDbContextFixture _assetsDbContextFixture;
        private readonly OriginalBlocksProviderFixture _originalBlocksProviderFixture;
        private readonly ITestOutputHelper _output;

        #endregion

        #region Constructor

        public ModelBlockItemsDeserializationSqlTest(
            AssetsDbContextFixture assetsDbContextFixture, 
            OriginalBlocksProviderFixture originalBlocksProviderFixture, 
            ITestOutputHelper output) : 
            base(originalBlocksProviderFixture.OriginalBlocksProvider.GetBlock<ModelBlockItem>(ModelBlockIdNames.Default))
        {
            _assetsDbContextFixture = assetsDbContextFixture;
            _originalBlocksProviderFixture = originalBlocksProviderFixture;
            _output = output;
        }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override void CompareItemInternal(int index)
        {
            ModelBlockItem modelBlockItem = DeserializeItem(index, out ByteSerializerContext context);

            var deserialized = new DbModelStructures(index);
            deserialized.Load(context.Graph);

            var fromDatabase = new DbModelStructures(index);
            fromDatabase.Load(_assetsDbContextFixture.AssetsDbContext);

            Assert.Equal(fromDatabase, deserialized);
        }

        protected override void PrintItemIndex(int index) { }

        protected override void PrintItemName(string nameString) => 
            _output.WriteLine(nameString);

        protected override void PrintItemDone() { }

        #endregion

        #region Methods ([Fact])

        [Fact]
        public void Test_000() => CompareItem(0);
        [Fact]
        public void Test_001() => CompareItem(1);
        [Fact]
        public void Test_002() => CompareItem(2);
        [Fact]
        public void Test_003() => CompareItem(3);
        [Fact]
        public void Test_004() => CompareItem(4);
        [Fact]
        public void Test_005() => CompareItem(5);
        [Fact]
        public void Test_006() => CompareItem(6);
        [Fact]
        public void Test_007() => CompareItem(7);
        [Fact]
        public void Test_008() => CompareItem(8);
        [Fact]
        public void Test_009() => CompareItem(9);
        [Fact]
        public void Test_010() => CompareItem(10);
        [Fact]
        public void Test_011() => CompareItem(11);
        [Fact]
        public void Test_012() => CompareItem(12);
        [Fact]
        public void Test_013() => CompareItem(13);
        [Fact]
        public void Test_014() => CompareItem(14);
        [Fact]
        public void Test_015() => CompareItem(15);
        [Fact]
        public void Test_016() => CompareItem(16);
        [Fact]
        public void Test_017() => CompareItem(17);
        [Fact]
        public void Test_018() => CompareItem(18);
        [Fact]
        public void Test_019() => CompareItem(19);
        [Fact]
        public void Test_020() => CompareItem(20);
        [Fact]
        public void Test_021() => CompareItem(21);
        [Fact]
        public void Test_022() => CompareItem(22);
        [Fact]
        public void Test_023() => CompareItem(23);
        [Fact]
        public void Test_024() => CompareItem(24);
        [Fact]
        public void Test_025() => CompareItem(25);
        [Fact]
        public void Test_026() => CompareItem(26);
        [Fact]
        public void Test_027() => CompareItem(27);
        [Fact]
        public void Test_028() => CompareItem(28);
        [Fact]
        public void Test_029() => CompareItem(29);
        [Fact]
        public void Test_030() => CompareItem(30);
        [Fact]
        public void Test_031() => CompareItem(31);
        [Fact]
        public void Test_032() => CompareItem(32);
        [Fact]
        public void Test_033() => CompareItem(33);
        [Fact]
        public void Test_034() => CompareItem(34);
        [Fact]
        public void Test_035() => CompareItem(35);
        [Fact]
        public void Test_036() => CompareItem(36);
        [Fact]
        public void Test_037() => CompareItem(37);
        [Fact]
        public void Test_038() => CompareItem(38);
        [Fact]
        public void Test_039() => CompareItem(39);
        [Fact]
        public void Test_040() => CompareItem(40);
        [Fact]
        public void Test_041() => CompareItem(41);
        [Fact]
        public void Test_042() => CompareItem(42);
        [Fact]
        public void Test_043() => CompareItem(43);
        [Fact]
        public void Test_044() => CompareItem(44);
        [Fact]
        public void Test_045() => CompareItem(45);
        [Fact]
        public void Test_046() => CompareItem(46);
        [Fact]
        public void Test_047() => CompareItem(47);
        [Fact]
        public void Test_048() => CompareItem(48);
        [Fact]
        public void Test_049() => CompareItem(49);
        [Fact]
        public void Test_050() => CompareItem(50);
        [Fact]
        public void Test_051() => CompareItem(51);
        [Fact]
        public void Test_052() => CompareItem(52);
        [Fact]
        public void Test_053() => CompareItem(53);
        [Fact]
        public void Test_054() => CompareItem(54);
        [Fact]
        public void Test_055() => CompareItem(55);
        [Fact]
        public void Test_056() => CompareItem(56);
        [Fact]
        public void Test_057() => CompareItem(57);
        [Fact]
        public void Test_058() => CompareItem(58);
        [Fact]
        public void Test_059() => CompareItem(59);
        [Fact]
        public void Test_060() => CompareItem(60);
        [Fact]
        public void Test_061() => CompareItem(61);
        [Fact]
        public void Test_062() => CompareItem(62);
        [Fact]
        public void Test_063() => CompareItem(63);
        [Fact]
        public void Test_064() => CompareItem(64);
        [Fact]
        public void Test_065() => CompareItem(65);
        [Fact]
        public void Test_066() => CompareItem(66);
        [Fact]
        public void Test_067() => CompareItem(67);
        [Fact]
        public void Test_068() => CompareItem(68);
        [Fact]
        public void Test_069() => CompareItem(69);
        [Fact]
        public void Test_070() => CompareItem(70);
        [Fact]
        public void Test_071() => CompareItem(71);
        [Fact]
        public void Test_072() => CompareItem(72);
        [Fact]
        public void Test_073() => CompareItem(73);
        [Fact]
        public void Test_074() => CompareItem(74);
        [Fact]
        public void Test_075() => CompareItem(75);
        [Fact]
        public void Test_076() => CompareItem(76);
        [Fact]
        public void Test_077() => CompareItem(77);
        [Fact]
        public void Test_078() => CompareItem(78);
        [Fact]
        public void Test_079() => CompareItem(79);
        [Fact]
        public void Test_080() => CompareItem(80);
        [Fact]
        public void Test_081() => CompareItem(81);
        [Fact]
        public void Test_082() => CompareItem(82);
        [Fact]
        public void Test_083() => CompareItem(83);
        [Fact]
        public void Test_084() => CompareItem(84);
        [Fact]
        public void Test_085() => CompareItem(85);
        [Fact]
        public void Test_086() => CompareItem(86);
        [Fact]
        public void Test_087() => CompareItem(87);
        [Fact]
        public void Test_088() => CompareItem(88);
        [Fact]
        public void Test_089() => CompareItem(89);
        [Fact]
        public void Test_090() => CompareItem(90);
        [Fact]
        public void Test_091() => CompareItem(91);
        [Fact]
        public void Test_092() => CompareItem(92);
        [Fact]
        public void Test_093() => CompareItem(93);
        [Fact]
        public void Test_094() => CompareItem(94);
        [Fact]
        public void Test_095() => CompareItem(95);
        [Fact]
        public void Test_096() => CompareItem(96);
        [Fact]
        public void Test_097() => CompareItem(97);
        [Fact]
        public void Test_098() => CompareItem(98);
        [Fact]
        public void Test_099() => CompareItem(99);
        [Fact]
        public void Test_100() => CompareItem(100);
        [Fact]
        public void Test_101() => CompareItem(101);
        [Fact]
        public void Test_102() => CompareItem(102);
        [Fact]
        public void Test_103() => CompareItem(103);
        [Fact]
        public void Test_104() => CompareItem(104);
        [Fact]
        public void Test_105() => CompareItem(105);
        [Fact]
        public void Test_106() => CompareItem(106);
        [Fact]
        public void Test_107() => CompareItem(107);
        [Fact]
        public void Test_108() => CompareItem(108);
        [Fact]
        public void Test_109() => CompareItem(109);
        [Fact]
        public void Test_110() => CompareItem(110);
        [Fact]
        public void Test_111() => CompareItem(111);
        [Fact]
        public void Test_112() => CompareItem(112);
        [Fact]
        public void Test_113() => CompareItem(113);
        [Fact]
        public void Test_114() => CompareItem(114);
        [Fact]
        public void Test_115() => CompareItem(115);
        [Fact]
        public void Test_116() => CompareItem(116);
        [Fact]
        public void Test_117() => CompareItem(117);
        [Fact]
        public void Test_118() => CompareItem(118);
        [Fact]
        public void Test_119() => CompareItem(119);
        [Fact]
        public void Test_120() => CompareItem(120);
        [Fact]
        public void Test_121() => CompareItem(121);
        [Fact]
        public void Test_122() => CompareItem(122);
        [Fact]
        public void Test_123() => CompareItem(123);
        [Fact]
        public void Test_124() => CompareItem(124);
        [Fact]
        public void Test_125() => CompareItem(125);
        [Fact]
        public void Test_126() => CompareItem(126);
        [Fact]
        public void Test_127() => CompareItem(127);
        [Fact]
        public void Test_128() => CompareItem(128);
        [Fact]
        public void Test_129() => CompareItem(129);
        [Fact]
        public void Test_130() => CompareItem(130);
        [Fact]
        public void Test_131() => CompareItem(131);
        [Fact]
        public void Test_132() => CompareItem(132);
        [Fact]
        public void Test_133() => CompareItem(133);
        [Fact]
        public void Test_134() => CompareItem(134);
        [Fact]
        public void Test_135() => CompareItem(135);
        [Fact]
        public void Test_136() => CompareItem(136);
        [Fact]
        public void Test_137() => CompareItem(137);
        [Fact]
        public void Test_138() => CompareItem(138);
        [Fact]
        public void Test_139() => CompareItem(139);
        [Fact]
        public void Test_140() => CompareItem(140);
        [Fact]
        public void Test_141() => CompareItem(141);
        [Fact]
        public void Test_142() => CompareItem(142);
        [Fact]
        public void Test_143() => CompareItem(143);
        [Fact]
        public void Test_144() => CompareItem(144);
        [Fact]
        public void Test_145() => CompareItem(145);
        [Fact]
        public void Test_146() => CompareItem(146);
        [Fact]
        public void Test_147() => CompareItem(147);
        [Fact]
        public void Test_148() => CompareItem(148);
        [Fact]
        public void Test_149() => CompareItem(149);
        [Fact]
        public void Test_150() => CompareItem(150);
        [Fact]
        public void Test_151() => CompareItem(151);
        [Fact]
        public void Test_152() => CompareItem(152);
        [Fact]
        public void Test_153() => CompareItem(153);
        [Fact]
        public void Test_154() => CompareItem(154);
        [Fact]
        public void Test_155() => CompareItem(155);
        [Fact]
        public void Test_156() => CompareItem(156);
        [Fact]
        public void Test_157() => CompareItem(157);
        [Fact]
        public void Test_158() => CompareItem(158);
        [Fact]
        public void Test_159() => CompareItem(159);
        [Fact]
        public void Test_160() => CompareItem(160);
        [Fact]
        public void Test_161() => CompareItem(161);
        [Fact]
        public void Test_162() => CompareItem(162);
        [Fact]
        public void Test_163() => CompareItem(163);
        [Fact]
        public void Test_164() => CompareItem(164);
        [Fact]
        public void Test_165() => CompareItem(165);
        [Fact]
        public void Test_166() => CompareItem(166);
        [Fact]
        public void Test_167() => CompareItem(167);
        [Fact]
        public void Test_168() => CompareItem(168);
        [Fact]
        public void Test_169() => CompareItem(169);
        [Fact]
        public void Test_170() => CompareItem(170);
        [Fact]
        public void Test_171() => CompareItem(171);
        [Fact]
        public void Test_172() => CompareItem(172);
        [Fact]
        public void Test_173() => CompareItem(173);
        [Fact]
        public void Test_174() => CompareItem(174);
        [Fact]
        public void Test_175() => CompareItem(175);
        [Fact]
        public void Test_176() => CompareItem(176);
        [Fact]
        public void Test_177() => CompareItem(177);
        [Fact]
        public void Test_178() => CompareItem(178);
        [Fact]
        public void Test_179() => CompareItem(179);
        [Fact]
        public void Test_180() => CompareItem(180);
        [Fact]
        public void Test_181() => CompareItem(181);
        [Fact]
        public void Test_182() => CompareItem(182);
        [Fact]
        public void Test_183() => CompareItem(183);
        [Fact]
        public void Test_184() => CompareItem(184);
        [Fact]
        public void Test_185() => CompareItem(185);
        [Fact]
        public void Test_186() => CompareItem(186);
        [Fact]
        public void Test_187() => CompareItem(187);
        [Fact]
        public void Test_188() => CompareItem(188);
        [Fact]
        public void Test_189() => CompareItem(189);
        [Fact]
        public void Test_190() => CompareItem(190);
        [Fact]
        public void Test_191() => CompareItem(191);
        [Fact]
        public void Test_192() => CompareItem(192);
        [Fact]
        public void Test_193() => CompareItem(193);
        [Fact]
        public void Test_194() => CompareItem(194);
        [Fact]
        public void Test_195() => CompareItem(195);
        [Fact]
        public void Test_196() => CompareItem(196);
        [Fact]
        public void Test_197() => CompareItem(197);
        [Fact]
        public void Test_198() => CompareItem(198);
        [Fact]
        public void Test_199() => CompareItem(199);
        [Fact]
        public void Test_200() => CompareItem(200);
        [Fact]
        public void Test_201() => CompareItem(201);
        [Fact]
        public void Test_202() => CompareItem(202);
        [Fact]
        public void Test_203() => CompareItem(203);
        [Fact]
        public void Test_204() => CompareItem(204);
        [Fact]
        public void Test_205() => CompareItem(205);
        [Fact]
        public void Test_206() => CompareItem(206);
        [Fact]
        public void Test_207() => CompareItem(207);
        [Fact]
        public void Test_208() => CompareItem(208);
        [Fact]
        public void Test_209() => CompareItem(209);
        [Fact]
        public void Test_210() => CompareItem(210);
        [Fact]
        public void Test_211() => CompareItem(211);
        [Fact]
        public void Test_212() => CompareItem(212);
        [Fact]
        public void Test_213() => CompareItem(213);
        [Fact]
        public void Test_214() => CompareItem(214);
        [Fact]
        public void Test_215() => CompareItem(215);
        [Fact]
        public void Test_216() => CompareItem(216);
        [Fact]
        public void Test_217() => CompareItem(217);
        [Fact]
        public void Test_218() => CompareItem(218);
        [Fact]
        public void Test_219() => CompareItem(219);
        [Fact]
        public void Test_220() => CompareItem(220);
        [Fact]
        public void Test_221() => CompareItem(221);
        [Fact]
        public void Test_222() => CompareItem(222);
        [Fact]
        public void Test_223() => CompareItem(223);
        [Fact]
        public void Test_224() => CompareItem(224);
        [Fact]
        public void Test_225() => CompareItem(225);
        [Fact]
        public void Test_226() => CompareItem(226);
        [Fact]
        public void Test_227() => CompareItem(227);
        [Fact]
        public void Test_228() => CompareItem(228);
        [Fact]
        public void Test_229() => CompareItem(229);
        [Fact]
        public void Test_230() => CompareItem(230);
        [Fact]
        public void Test_231() => CompareItem(231);
        [Fact]
        public void Test_232() => CompareItem(232);
        [Fact]
        public void Test_233() => CompareItem(233);
        [Fact]
        public void Test_234() => CompareItem(234);
        [Fact]
        public void Test_235() => CompareItem(235);
        [Fact]
        public void Test_236() => CompareItem(236);
        [Fact]
        public void Test_237() => CompareItem(237);
        [Fact]
        public void Test_238() => CompareItem(238);
        [Fact]
        public void Test_239() => CompareItem(239);
        [Fact]
        public void Test_240() => CompareItem(240);
        [Fact]
        public void Test_241() => CompareItem(241);
        [Fact]
        public void Test_242() => CompareItem(242);
        [Fact]
        public void Test_243() => CompareItem(243);
        [Fact]
        public void Test_244() => CompareItem(244);
        [Fact]
        public void Test_245() => CompareItem(245);
        [Fact]
        public void Test_246() => CompareItem(246);
        [Fact]
        public void Test_247() => CompareItem(247);
        [Fact]
        public void Test_248() => CompareItem(248);
        [Fact]
        public void Test_249() => CompareItem(249);
        [Fact]
        public void Test_250() => CompareItem(250);
        [Fact]
        public void Test_251() => CompareItem(251);
        [Fact]
        public void Test_252() => CompareItem(252);
        [Fact]
        public void Test_253() => CompareItem(253);
        [Fact]
        public void Test_254() => CompareItem(254);
        [Fact]
        public void Test_255() => CompareItem(255);
        [Fact]
        public void Test_256() => CompareItem(256);
        [Fact]
        public void Test_257() => CompareItem(257);
        [Fact]
        public void Test_258() => CompareItem(258);
        [Fact]
        public void Test_259() => CompareItem(259);
        [Fact]
        public void Test_260() => CompareItem(260);
        [Fact]
        public void Test_261() => CompareItem(261);
        [Fact]
        public void Test_262() => CompareItem(262);
        [Fact]
        public void Test_263() => CompareItem(263);
        [Fact]
        public void Test_264() => CompareItem(264);
        [Fact]
        public void Test_265() => CompareItem(265);
        [Fact]
        public void Test_266() => CompareItem(266);
        [Fact]
        public void Test_267() => CompareItem(267);
        [Fact]
        public void Test_268() => CompareItem(268);
        [Fact]
        public void Test_269() => CompareItem(269);
        [Fact]
        public void Test_270() => CompareItem(270);
        [Fact]
        public void Test_271() => CompareItem(271);
        [Fact]
        public void Test_272() => CompareItem(272);
        [Fact]
        public void Test_273() => CompareItem(273);
        [Fact]
        public void Test_274() => CompareItem(274);
        [Fact]
        public void Test_275() => CompareItem(275);
        [Fact]
        public void Test_276() => CompareItem(276);
        [Fact]
        public void Test_277() => CompareItem(277);
        [Fact]
        public void Test_278() => CompareItem(278);
        [Fact]
        public void Test_279() => CompareItem(279);
        [Fact]
        public void Test_280() => CompareItem(280);
        [Fact]
        public void Test_281() => CompareItem(281);
        [Fact]
        public void Test_282() => CompareItem(282);
        [Fact]
        public void Test_283() => CompareItem(283);
        [Fact]
        public void Test_284() => CompareItem(284);
        [Fact]
        public void Test_285() => CompareItem(285);
        [Fact]
        public void Test_286() => CompareItem(286);
        [Fact]
        public void Test_287() => CompareItem(287);
        [Fact]
        public void Test_288() => CompareItem(288);
        [Fact]
        public void Test_289() => CompareItem(289);
        [Fact]
        public void Test_290() => CompareItem(290);
        [Fact]
        public void Test_291() => CompareItem(291);
        [Fact]
        public void Test_292() => CompareItem(292);
        [Fact]
        public void Test_293() => CompareItem(293);
        [Fact]
        public void Test_294() => CompareItem(294);
        [Fact]
        public void Test_295() => CompareItem(295);
        [Fact]
        public void Test_296() => CompareItem(296);
        [Fact]
        public void Test_297() => CompareItem(297);
        [Fact]
        public void Test_298() => CompareItem(298);
        [Fact]
        public void Test_299() => CompareItem(299);
        [Fact]
        public void Test_300() => CompareItem(300);
        [Fact]
        public void Test_301() => CompareItem(301);
        [Fact]
        public void Test_302() => CompareItem(302);
        [Fact]
        public void Test_303() => CompareItem(303);
        [Fact]
        public void Test_304() => CompareItem(304);
        [Fact]
        public void Test_305() => CompareItem(305);
        [Fact]
        public void Test_306() => CompareItem(306);
        [Fact]
        public void Test_307() => CompareItem(307);
        [Fact]
        public void Test_308() => CompareItem(308);
        [Fact]
        public void Test_309() => CompareItem(309);
        [Fact]
        public void Test_310() => CompareItem(310);
        [Fact]
        public void Test_311() => CompareItem(311);
        [Fact]
        public void Test_312() => CompareItem(312);
        [Fact]
        public void Test_313() => CompareItem(313);
        [Fact]
        public void Test_314() => CompareItem(314);
        [Fact]
        public void Test_315() => CompareItem(315);
        [Fact]
        public void Test_316() => CompareItem(316);
        [Fact]
        public void Test_317() => CompareItem(317);
        [Fact]
        public void Test_318() => CompareItem(318);
        [Fact]
        public void Test_319() => CompareItem(319);
        [Fact]
        public void Test_320() => CompareItem(320);
        [Fact]
        public void Test_321() => CompareItem(321);
        [Fact]
        public void Test_322() => CompareItem(322);
        
        #endregion
    }
}