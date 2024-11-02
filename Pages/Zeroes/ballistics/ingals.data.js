const INGALS = {
	v: [
		5000, 4980, 4960, 4940, 4920, 4900, 4880, 4860, 4840, 4820, 4800, 4780, 4760, 4740, 4720, 4700, 4680, 4660, 4640, 4620, 4600, 4580, 4560, 4540, 4520, 4500, 4480, 4460, 4440, 4420, 4400, 4380, 4360, 4340, 4320, 4300, 4280, 4260, 4240, 4220, 4200, 4180, 4160, 4140, 4120, 4100, 4080, 4060, 4040, 4020, 4000, 3980, 3960, 3940, 3920, 3900, 3880, 3860, 3840, 3820, 3800, 3780, 3760, 3740, 3720, 3700, 3680, 3660, 3640, 3620, 3600, 3580, 3560, 3540, 3520, 3500, 3480, 3460, 3440, 3420, 3400, 3390, 3380, 3370, 3360, 3350, 3340, 3330, 3320, 3310, 3300, 3290, 3280, 3270, 3260, 3250, 3240, 3230, 3220, 3210, 3200, 3190, 3180, 3170, 3160, 3150, 3140, 3130, 3120, 3110, 3100, 3090, 3080, 3070, 3060, 3050, 3040, 3030, 3020, 3010, 3000, 2990, 2980, 2970, 2960, 2950, 2940, 2930, 2920, 2910, 2900, 2890, 2880, 2870, 2860, 2850, 2840, 2830, 2820, 2810, 2800, 2790, 2780, 2770, 2760, 2750, 2740, 2730, 2720, 2710, 2700, 2690, 2680, 2670, 2660, 2650, 2640, 2630, 2620, 2610, 2600, 2590, 2580, 2570, 2560, 2550, 2540, 2530, 2520, 2510, 2500, 2490, 2480, 2470, 2460, 2450, 2440, 2430, 2420, 2410, 2400, 2390, 2380, 2370, 2360, 2350, 2340, 2330, 2320, 2310, 2300, 2290, 2280, 2270, 2260, 2250, 2240, 2230, 2220, 2210, 2200, 2190, 2180, 2170, 2160, 2150, 2140, 2130, 2120, 2110, 2100, 2090, 2080, 2070, 2060, 2050, 2040, 2030, 2020, 2010, 2000, 1990, 1980, 1970, 1960, 1950, 1940, 1930, 1920, 1910, 1900, 1890, 1880, 1870, 1860, 1850, 1840, 1830, 1820, 1810, 1800, 1790, 1780, 1770, 1760, 1750, 1740, 1730, 1720, 1710, 1700, 1690, 1680, 1670, 1660, 1650, 1640, 1630, 1620, 1610, 1600, 1595, 1590, 1585, 1580, 1575, 1570, 1565, 1560, 1555, 1550, 1545, 1540, 1535, 1530, 1525, 1520, 1515, 1510, 1505, 1500, 1495, 1490, 1485, 1480, 1475, 1470, 1465, 1460, 1455, 1450, 1445, 1440, 1435, 1430, 1425, 1420, 1415, 1410, 1405, 1400, 1395, 1390, 1385, 1380, 1375, 1370, 1365, 1360, 1355, 1350, 1345, 1340, 1335, 1330, 1325, 1320, 1315, 1310, 1305, 1300, 1298, 1296, 1294, 1292, 1290, 1288, 1286, 1284, 1282, 1280, 1278, 1276, 1274, 1272, 1270, 1268, 1266, 1264, 1262, 1260, 1258, 1256, 1254, 1252, 1250, 1248, 1246, 1244, 1242, 1240, 1238, 1236, 1234, 1232, 1230, 1228, 1226, 1224, 1222, 1220, 1218, 1216, 1214, 1212, 1210, 1208, 1206, 1204, 1202, 1200, 1198, 1196, 1194, 1192, 1190, 1188, 1186, 1184, 1182, 1180, 1178, 1176, 1174, 1172, 1170, 1168, 1166, 1164, 1162, 1160, 1158, 1156, 1154, 1152, 1150, 1148, 1146, 1144, 1142, 1140, 1138, 1136, 1134, 1132, 1130, 1128, 1126, 1124, 1122, 1120, 1118, 1116, 1114, 1112, 1110, 1108, 1106, 1104, 1102, 1100, 1099, 1098, 1097, 1096, 1095, 1094, 1093, 1092, 1091, 1090, 1089, 1088, 1087, 1086, 1085, 1084, 1083, 1082, 1081, 1080, 1079, 1078, 1077, 1076, 1075, 1074, 1073, 1072, 1071, 1070, 1069, 1068, 1067, 1066, 1065, 1064, 1063, 1062, 1061, 1060, 1059, 1058, 1057, 1056, 1055, 1054, 1053, 1052, 1051, 1050, 1049, 1048, 1047, 1046, 1045, 1044, 1043, 1042, 1041, 1040, 1039, 1038, 1037, 1036, 1035, 1034, 1033, 1032, 1031, 1030, 1029, 1028, 1027, 1026, 1025, 1024, 1023, 1022, 1021, 1020, 1019, 1018, 1017, 1016, 1015, 1014, 1013, 1012, 1011, 1010, 1009, 1008, 1007, 1006, 1005, 1004, 1003, 1002, 1001, 1000, 999, 998, 997, 996, 995, 994, 993, 992, 991, 990, 989, 988, 987, 986, 985, 984, 983, 982, 981, 980, 979, 978, 977, 976, 975, 974, 973, 972, 971, 970, 969, 968, 967, 966, 965, 964, 963, 962, 961, 960, 959, 958, 957, 956, 955, 954, 953, 952, 951, 950, 949, 948, 947, 946, 945, 944, 943, 942, 941, 940, 939, 938, 937, 936, 935, 934, 933, 932, 931, 930, 929, 928, 927, 926, 925, 924, 923, 922, 921, 920, 919, 918, 917, 916, 915, 914, 913, 912, 911, 910, 909, 908, 907, 906, 905, 904, 903, 902, 901, 900, 899, 898, 897, 896, 895, 894, 893, 892, 891, 890, 889, 888, 887, 886, 885, 884, 883, 882, 881, 880, 879, 878, 877, 876, 875, 874, 873, 872, 871, 870, 869, 868, 867, 866, 865, 864, 863, 862, 861, 860, 859, 858, 857, 856, 855, 854, 853, 852, 851, 850, 849, 848, 847, 846, 845, 844, 843, 842, 841, 840, 839, 838, 837, 836, 835, 834, 833, 832, 831, 830, 829, 828, 827, 826, 825, 824, 823, 822, 821, 820, 819, 818, 817, 816, 815, 814, 813, 812, 811, 810, 809, 808, 807, 806, 805, 804, 803, 802, 801, 800, 799, 798, 797, 796, 795, 794, 793, 792, 791, 790, 789, 788, 787, 786, 785, 784, 783, 782, 781, 780, 779, 778, 777, 776, 775, 774, 773, 772, 771, 770, 769, 768, 767, 766, 765, 764, 763, 762, 761, 760, 759, 758, 757, 756, 755, 754, 753, 752, 751, 750, 749, 748, 747, 746, 745, 744, 743, 742, 741, 740, 739, 738, 737, 736, 735, 734, 733, 732, 731, 730, 729, 728, 727, 726, 725, 724, 723, 722, 721, 720, 719, 718, 717, 716, 715, 714, 713, 712, 711, 710, 709, 708, 707, 706, 705, 704, 703, 702, 701, 700, 699, 698, 697, 696, 695, 694, 693, 692, 691, 690, 689, 688, 687, 686, 685, 684, 683, 682, 681, 680, 679, 678, 677, 676, 675, 674, 673, 672, 671, 670, 669, 668, 667, 666, 665, 664, 663, 662, 661, 660, 659, 658, 657, 656, 655, 654, 653, 652, 651, 650, 649, 648, 647, 646, 645, 644, 643, 642, 641, 640, 639, 638, 637, 636, 635, 634, 633, 632, 631, 630, 629, 628, 627, 626, 625, 624, 623, 622, 621, 620, 619, 618, 617, 616, 615, 614, 613, 612, 611, 610, 609, 608, 607, 606, 605, 604, 603, 602, 601, 600, 599, 598, 597, 596, 595, 594, 593, 592, 591, 590, 589, 588, 587, 586, 585, 584, 583, 582, 581, 580, 579, 578, 577, 576, 575, 574, 573, 572, 571, 570, 569, 568, 567, 566, 565, 564, 563, 562, 561, 560, 559, 558, 557, 556, 555, 554, 553, 552, 551, 550, 549, 548, 547, 546, 545, 544, 543, 542, 541, 540, 539, 538, 537, 536, 535, 534, 533, 532, 531, 530, 529, 528, 527, 526, 525, 524, 523, 522, 521, 520, 519, 518, 517, 516, 515, 514, 513, 512, 511, 510, 509, 508, 507, 506, 505, 504, 503, 502, 501, 500, 499, 498, 497, 496, 495, 494, 493, 492, 491, 490, 489, 488, 487, 486, 485, 484, 483, 482, 481, 480, 479, 478, 477, 476, 475, 474, 473, 472, 471, 470, 469, 468, 467, 466, 465, 464, 463, 462, 461, 460, 459, 458, 457, 456, 455, 454, 453, 452, 451, 450, 449, 448, 447, 446, 445, 444, 443, 442, 441, 440, 439, 438, 437, 436, 435, 434, 433, 432, 431, 430, 429, 428, 427, 426, 425, 424, 423, 422, 421, 420, 419, 418, 417, 416, 415, 414, 413, 412, 411, 410, 409, 408, 407, 406, 405, 404, 403, 402, 401, 400, 399, 398, 397, 396, 395, 394, 393, 392, 391, 390, 389, 388, 387, 386, 385, 384, 383, 382, 381, 380, 379, 378, 377, 376, 375, 374, 373, 372, 371, 370, 369, 368, 367, 366, 365, 364, 363, 362, 361, 360, 359, 358, 357, 356, 355, 354, 353, 352, 351, 350, 349, 348, 347, 346, 345, 344, 343, 342, 341, 340, 339, 338, 337, 336, 335, 334, 333, 332, 331, 330, 329, 328, 327, 326, 325, 324, 323, 322, 321, 320, 319, 318, 317, 316, 315, 314, 313, 312, 311, 310, 309, 308, 307, 306, 305, 304, 303, 302, 301, 300, 299, 298, 297, 296, 295, 294, 293, 292, 291, 290, 289, 288, 287, 286, 285, 284, 283, 282, 281, 280, 279, 278, 277, 276, 275, 274, 273, 272, 271, 270, 269, 268, 267, 266, 265, 264, 263, 262, 261, 260, 259, 258, 257, 256, 255, 254, 253, 252, 251, 250, 249, 248, 247, 246, 245, 244, 243, 242, 241, 240, 239, 238, 237, 236, 235, 234, 233, 232, 231, 230, 229, 228, 227, 226, 225, 224, 223, 222, 221, 220, 219, 218, 217, 216, 215, 214, 213, 212, 211, 210, 209, 208, 207, 206, 205, 204, 203, 202, 201, 200, 199, 198, 197, 196, 195, 194, 193, 192, 191, 190, 189, 188, 187, 186, 185, 184, 183, 182, 181, 180, 179, 178, 177, 176, 175, 174, 173, 172, 171, 170, 169, 168, 167, 166, 165, 164, 163, 162, 161, 160, 159, 158, 157, 156, 155, 154, 153, 152, 151, 150, 149, 148, 147, 146, 145, 144, 143, 142, 141, 140, 139, 138, 137, 136, 135, 134, 133, 132, 131, 130, 129, 128, 127, 126, 125, 124, 123, 122, 121, 120, 119, 118, 117, 116, 115, 114, 113, 112, 111, 110, 109, 108, 107, 106, 105, 104, 103, 102, 101, 100
	],
	s: [
		-3740, -3424.5, -3378.9, -3333.2, -3287.4, -3241.4, -3195.4, -3149.3, -3103.1, -3056.8, -3010.3, -2963.8, -2917.2, -2870.4, -2823.6, -2776.6, -2729.5, -2682.3, -2635, -2587.6, -2540.1, -2492.4, -2444.7, -2396.8, -2348.9, -2300.7, -2252.5, -2204.1, -2155.7, -2107.1, -2058.4, -2009.6, -1960.6, -1911.5, -1862.3, -1813, -1763.6, -1714, -1664.3, -1614.5, -1564.5, -1514.3, -1464.1, -1413.7, -1363.2, -1312.7, -1261.9, -1211, -1160, -1108, -1057.5, -1006.1, -954.5, -902.8, -850.9, -798.8, -746.6, -694.4, -641.8, -589.2, -536.4, -483.5, -430.4, -377.2, -323.8, -270.2, -216.5, -162.6, -108.6, -54.4, 0, 54.5, 109.2, 164.1, 219.2, 274.4, 329.8, 385.3, 441.1, 497, 553.1, 581.3, 609.4, 637.6, 665.8, 794.1, 722.5, 750.9, 779.3, 807.8, 836.4, 865, 893.6, 922.3, 951, 979.8, 1008.6, 1037.5, 1066.4, 1095.4, 1124.4, 1153.5, 1182.6, 1211.8, 1241, 1270.3, 1299.6, 1329, 1358.4, 1387.9, 1417.4, 1447, 1476.6, 1506.3, 1536.1, 1565.9, 1595.7, 1625.6, 1655.6, 1685.6, 1715.7, 1745.8, 1776, 1806.2, 1836.5, 1866.8, 1897.2, 1927.7, 1958.2, 1988.8, 2019.4, 2050.1, 2080.9, 2111.7, 2142.6, 2173.5, 2204.5, 2235.6, 2266.7, 2297.9, 2329.1, 2360.4, 2391.7, 2423.1, 2454.6, 2486.1, 2517.7, 2549.4, 2581.1, 2612.9, 2644.8, 2676.7, 2708.7, 2740.8, 2772.9, 2805.1, 2837.4, 2869.7, 2902.1, 2934.6, 2967.1, 2999.7, 3032.4, 3065.3, 3098.2, 3131.2, 3164.3, 3197.5, 3230.8, 3264.1, 3297.6, 3331.2, 3364.8, 3398.6, 3432.4, 3466.4, 3500.4, 3534.6, 3568.8, 3603.1, 3637.6, 3672.1, 3706.7, 3741.5, 3776.3, 3811.3, 3846.3, 3881.5, 3916.8, 3952.1, 3987.6, 4023.2, 4058.9, 4094.7, 4130.6, 4166.6, 4202.8, 4239, 4275.4, 4311.8, 4348.4, 4385.1, 4422, 4458.9, 4496, 4533.2, 4570.5, 4607.9, 4645.5, 4683.1, 4720.9, 4758.9, 4797, 4835.2, 4873.5, 4911.9, 4950.5, 4989.2, 5028.1, 5067, 5106.1, 5145.3, 5184.7, 5224.3, 5263.9, 5303.7, 5343.7, 5383.8, 5424, 5464.4, 5505, 5545.7, 5586.5, 5627.5, 5668.6, 5709.9, 5751.4, 5793, 5834.8, 5876.7, 5918.8, 5961.1, 6003.7, 6046.5, 6089.6, 6132.9, 6176.4, 6220.2, 6264.3, 6308.6, 6353.1, 6397.9, 6443, 6488.4, 6534.1, 6580, 6626.2, 6672.7, 6719.4, 6766.5, 6813.8, 6837.6, 6861.5, 6885.4, 6909.4, 6933.5, 6957.6, 6981.9, 7006.2, 7030.6, 7055.1, 7079.7, 7104.3, 7129, 7153.8, 7178.7, 7203.6, 7228.6, 7253.7, 7278.9, 7304.2, 7329.7, 7355.2, 7380.8, 7406.4, 7432.1, 7457.9, 7483.8, 7509.8, 7535.9, 7562, 7588.3, 7614.6, 7641, 7667.5, 7694.2, 7720.9, 7747.7, 7774.6, 7801.5, 7828.5, 7855.7, 7883, 7910.4, 7937.9, 7965.5, 7993.1, 8021, 8049.1, 8077.5, 8106.1, 8134.9, 8163.9, 8193.1, 8222.5, 8252.1, 8282, 8312.1, 8342.4, 8373, 8403.8, 8416.2, 8428.6, 8441.1, 8453.6, 8466.1, 8478.7, 8491.3, 8504, 8516.7, 8529.4, 8542.2, 8555, 8567.9, 8580.8, 8593.7, 8606.7, 8619.7, 8632.8, 8645.9, 8659, 8672.2, 8685.4, 8698.6, 8711.9, 8725.3, 8738.7, 8752.1, 8765.6, 8779.1, 8792.7, 8806.3, 8820, 8833.7, 8847.5, 8861.3, 8875.1, 8889, 8903, 8917.1, 8931.3, 8945.6, 8960, 8974.5, 8989.1, 9003.8, 9018.5, 9033.4, 9048.4, 9063.4, 9078.6, 9093.9, 9109.3, 9124.7, 9140.3, 9156, 9171.8, 9187.7, 9203.7, 9219.8, 9236, 9252.3, 9268.8, 9285.3, 9302, 9318.8, 9335.7, 9352.7, 9369.9, 9387.1, 9404.5, 9422, 9439.6, 9457.3, 9475.2, 9493.2, 9511.3, 9529.5, 9547.9, 9566.4, 9585, 9603.7, 9622.6, 9641.6, 9660.8, 9680.1, 9699.5, 9719.1, 9738.8, 9758.6, 9778.6, 9798.7, 9819, 9839.4, 9860, 9880.7, 9901.6, 9922.6, 9943.8, 9965.1, 9986.6, 9997.4, 10008.3, 10019.2, 10030.1, 10041, 10052, 10063, 10074.1, 10085.2, 10096.4, 10107.6, 10118.8, 10130.1, 10141.4, 10152.7, 10164.1, 10175.6, 10187.1, 10198.6, 10210.2, 10221.8, 10233.5, 10245.2, 10257, 10268.8, 10280.6, 10292.5, 10304.4, 10316.4, 10328.4, 10340.5, 10352.6, 10364.7, 10376.9, 10389.2, 10401.5, 10413.8, 10426.2, 10438.6, 10451.1, 10463.6, 10476.2, 10488.8, 10501.5, 10514.2, 10527, 10539.8, 10550.6, 10565.5, 10578.5, 10591.5, 10604.5, 10617.6, 10630.8, 10644, 10657.2, 10670.5, 10683.9, 10697.3, 10710.8, 10724.3, 10737.9, 10751.5, 10765.2, 10779, 10792.8, 10806.6, 10820.5, 10834.4, 10848.4, 10862.4, 10876.5, 10890.7, 10904.9, 10919.1, 10933.4, 10947.8, 10962.2, 10976.7, 10991.3, 11005.9, 11020.6, 11035.3, 11050.1, 11064.9, 11079.8, 11094.8, 11109.8, 11124.9, 11140, 11155.2, 11170.4, 11185.7, 11201.1, 11216.5, 11232, 11247.6, 11263.2, 11278.9, 11294.7, 11310.5, 11326.4, 11342.4, 11358.4, 11374.5, 11390.6, 11406.8, 11423.1, 11439.4, 11455.8, 11472.3, 11488.8, 11505.4, 11522, 11538.7, 11555.5, 11572.4, 11589.3, 11606.3, 11623.4, 11640.6, 11657.8, 11675.1, 11692.4, 11709.8, 11727.3, 11744.9, 11762.6, 11780.3, 11798.1, 11816, 11834, 11852, 11870, 11888.1, 11906.2, 11924.3, 11942.5, 11960.7, 11979, 11997.3, 12015.6, 12034, 12052.4, 12070.8, 12089.3, 12107.9, 12126.4, 12145, 12163.7, 12182.4, 12201.1, 12219.9, 12238.7, 12257.5, 12276.4, 12295.4, 12314.3, 12333.3, 12352.4, 12371.5, 12390.6, 12409, 12429, 12448.2, 12467.5, 12486.8, 12506.2, 12525.6, 12545.1, 12564.6, 12584.2, 12603.8, 12623.4, 12643.1, 12662.8, 12682.6, 12702.4, 12722.2, 12742.1, 12762, 12782, 12802, 12822, 12842.1, 12862.3, 12882.5, 12902.7, 12923, 12943.3, 12963.7, 12984.1, 13004.5, 13025, 13045.6, 13066.2, 13086.8, 13107.5, 13128.2, 13149, 13169.8, 13190.7, 13211.6, 13232.6, 13253.6, 13274.7, 13295.8, 13316.9, 13338.1, 13359.4, 13380.7, 13402, 13423.4, 13444.9, 13466.4, 13487.9, 13509.5, 13531.1, 13552.8, 13574.5, 13596.3, 13618.1, 13640, 13661.9, 13683.9, 13705.9, 13728, 13750.1, 13772.3, 13794.5, 13816.8, 13839.1, 13861.5, 13883.9, 13906.4, 13929, 13951.6, 13974.2, 13996.9, 14019.7, 14042.5, 14065.4, 14088.3, 14111.3, 14134.3, 14157.4, 14180.5, 14203.7, 14226.9, 14250.2, 14273.5, 14296.9, 14320.4, 14343.9, 14367.5, 14391.1, 14414.8, 14438.5, 14462.3, 14486.2, 14510.1, 14534, 14558, 14582.1, 14606.2, 14630.4, 14654.7, 14679, 14703.4, 14727.8, 14752.3, 14776.8, 14801.4, 14826.1, 14850.8, 14875.6, 14900.5, 14925.4, 14950.4, 14975.4, 15000.5, 15025.6, 15050.8, 15076.1, 15101.5, 15126.9, 15152.4, 15177.9, 15203.5, 15229.1, 15254.8, 15280.6, 15306.4, 15332.3, 15358.3, 15384.3, 15410.4, 15436.6, 15462.8, 15489.1, 15515.5, 15541.9, 15568.4, 15594.9, 15621.5, 15648.2, 15675, 15701.8, 15728.7, 15755.6, 15782.7, 15809.8, 15836.9, 15864.1, 15891.3, 15918.6, 15945.9, 15973.2, 16000.6, 16028, 16055.4, 16082.9, 16110.4, 16137.9, 16165.5, 16193.1, 16220.7, 16248.4, 16276.1, 16303.9, 16331.7, 16359.5, 16387.4, 16415.3, 16443.2, 16471.2, 16499.2, 16527.3, 16555.4, 16583.5, 16611.6, 16639.8, 16668, 16696.3, 16724.6, 16752.9, 16781.3, 16809.7, 16838.2, 16866.7, 16895.2, 16923.8, 16952.4, 16981.1, 17009.8, 17038.5, 17067.3, 17096.1, 17124.9, 17153.8, 17182.7, 17211.7, 17240.7, 17269.7, 17298.8, 17327.9, 17357, 17386.2, 17415.4, 17444.7, 17474, 17503.3, 17532.7, 17562.2, 17591.7, 17621.2, 17650.7, 17680.3, 17710, 17739.7, 17769.5, 17799.3, 17829.1, 17859, 17888.9, 17918.9, 17948.9, 17978.9, 18009, 18039.1, 18069.2, 18099.3, 18129.5, 18159.8, 18190.1, 18220.4, 18250.8, 18281.2, 18311.7, 18342.2, 18372.8, 18403.4, 18434, 18464.7, 18495.5, 18526.3, 18557.1, 18588, 18618.9, 18649.9, 18680.9, 18711.9, 18743, 18774.1, 18805.3, 18836.6, 18867.9, 18899.2, 18930.6, 18962, 18993.5, 19025, 19056.6, 19088.2, 19119.9, 19151.6, 19183.3, 19215.1, 19247, 19278.9, 19310.8, 19342.8, 19374.8, 19406.9, 19439, 19471.2, 19503.4, 19535.7, 19568, 19600.4, 19632.8, 19665.3, 19697.8, 19730.4, 19736, 19795.7, 19828.4, 19861.2, 19894, 19926.9, 19959.8, 19992.8, 20025.8, 20058.9, 20092, 20125.2, 20158.5, 20191.8, 20225.1, 20258.5, 20291.9, 20325.4, 20359, 20392.6, 20426.2, 20459.9, 20493.7, 20527.5, 20561.4, 20595.3, 20629.3, 20663.3, 20697.4, 20731.5, 20765.7, 20800, 20834.3, 20868.6, 20903, 20937.5, 20972, 21006.6, 21041.2, 21075.9, 21110.6, 21145.4, 21180.3, 21215.2, 21250.2, 21285.2, 21320.3, 21355.4, 21390.6, 21425.9, 21416.2, 21496.6, 21532, 21567.5, 21603.1, 21638.7, 21674.4, 21710.1, 21745.9, 21781.7, 21817.6, 21853.6, 21889.6, 21925.7, 21961.9, 21998.1, 22034.4, 22070.7, 22107.1, 22143.5, 22180, 22216.6, 22253.3, 22290, 22326.8, 22363.6, 22400.5, 22437.5, 22474.5, 22511.6, 22548.7, 22585.9, 22623.2, 22660.6, 22698, 22735.5, 22773.1, 22810.7, 22848.4, 22886.1, 22924, 22961.9, 22999.8, 23037.8, 23075.9, 23114.1, 23152.3, 23190.6, 23228.9, 23267.3, 23305.8, 23344.4, 23383.1, 23421.8, 23460.5, 23499.4, 23538.3, 23577.3, 23616.4, 23655.5, 23694.7, 23734, 23773.3, 23812.7, 23852.2, 23891.8, 23931.4, 23971.1, 24010.9, 24050.8, 24090.7, 24130.7, 24170.8, 24210.9, 24251.2, 24291.5, 24331.9, 24372.4, 24412.9, 24453.5, 24494.2, 24535, 24575.9, 24616.8, 24657.8, 24698.9, 24740, 24781.2, 24822.5, 24863.9, 24905.4, 24946.9, 24988.6, 25030.3, 25072.1, 25114, 25156, 25198, 25240.2, 25282.4, 25324.7, 25367.1, 25409.6, 25452.2, 25494.9, 25537.6, 25580.4, 25623.3, 25666.3, 25709.3, 25752.5, 25795.7, 25839.1, 25882.5, 25926, 25969.6, 26013.3, 26057.1, 26100.9, 26144.9, 26188.9, 26233.1, 26277.3, 26321.7, 26366.1, 26410.6, 26455.2, 26499.9, 26544.6, 26589.5, 26634.5, 26679.5, 26724.7, 26770, 26815.3, 26860.8, 26906.3, 26952, 26997.7, 27043.6, 27089.5, 27135.5, 27181.7, 27227.9, 27274.3, 27320.7, 27367.2, 27413.9, 27460.6, 27507.4, 27554.4, 27601.4, 27648.6, 27695.8, 27743.2, 27790.7, 27838.3, 27885.9, 27933.7, 27981.6, 28029.6, 28077.7, 28125.9, 28174.3, 28222.7, 28271.3, 28320, 28368.7, 28417.6, 28466.6, 28515.7, 28565, 28614.3, 28663.7, 28713.3, 28763, 28812.8, 28862.6, 28912.6, 28962.8, 29013, 29063.4, 29113.9, 29164.5, 29215.2, 29266.1, 29317.1, 29368.2, 29419.4, 29470.7, 29522.2, 29573.8, 29625.6, 29677.4, 29729.4, 29781.5, 29833.7, 29886, 29938.5, 29991.1, 30043.8, 30096.7, 30149.7, 30202.8, 30256.1, 30309.5, 30363, 30416.7, 30470.5, 30524.4, 30578.5, 30632.7, 30687, 30741.5, 30796.1, 30850.9, 30905.8, 30960.9, 31016.1, 31071.4, 31126.9, 31182.5, 31238.2, 31294.1, 31350.2, 31406.4, 31462.7, 31519.2, 31575.9, 31632.7, 31689.6, 31746.7, 31804, 31861.4, 31919, 31976.7, 32034.6, 32092.6, 32150.8, 32209.1, 32267.6, 32326.3, 32385.1, 32444.1, 32503.3, 32562.6, 32622.1, 32681.8, 32741.6, 32801.6, 32861.8, 32922.1, 32982.6, 33043.3, 33104.1, 33165.1, 33226.3, 33287.7, 33349.2, 33410.9, 33472.8, 33534.9, 33597.2, 33659.6, 33722.2, 33785, 33848, 33911.2, 33974.5, 34038, 34101.8, 34165.7, 34229.9, 34294.2, 34358.7, 34423.4, 34488.3, 34553.4, 34618.7, 34684.2, 34749.9, 34815.9, 34882, 34948.3, 35014.8, 35081.5, 35148.4, 35215.6, 35282.9, 35350.5, 35418.3, 35486.3, 35554.5, 35622.9, 35691.5, 35760.4, 35829.5, 35898.8, 35968.3, 36038.1, 36108.1, 36178.3, 36248.8, 36319.5, 36390.4, 36461.6, 36533, 36604.7, 36676.6, 36748.7, 36821.1, 36893.7, 36966.5, 37039.6, 37113, 37186.6, 37260.5, 37334.6, 37409, 37483.6, 37558.5, 37633.7, 37709.1, 37784.8, 37860.8, 37937, 38013.5, 38090.3, 38167.3, 38244.7, 38322.3, 38400.2, 38478.4, 38556.9, 38635.6, 38714.7, 38794.1, 38873.7, 38953.7, 39033.9, 39114.5, 39195.3, 39276.5, 39357.9, 39439.7, 39521.8, 39604.2, 39686.9, 39770, 39853.4, 39937.1, 40021.1, 40105.4, 40190.1, 40275.1, 40360.5, 40446.2, 40532.3, 40618.7, 40705.5, 40792.6, 40880, 40967.8, 41056, 41144.6, 41233.5, 41322.8, 41412.5, 41502.5, 41592.9, 41683.7, 41774.9, 41866.5, 41958.5, 42050.9, 42143.7, 42236.9, 42330.6, 42424.6, 42519.1, 42613.9, 42709, 42804.6, 42900.7, 42997.2, 43094.2, 43191.6, 43289.5, 43387.9, 43486.7, 43585.9, 43685.6, 43785.8, 43886.4, 43987.5, 44089.1, 44191.2, 44293.7, 44396.8, 44500.3, 44604.4, 44708.9, 44814, 44919.6, 45025.7, 45132.4, 45239.6, 45347.4, 45455.7, 45564.5, 45673.9, 45783.9, 45894.4, 46005.5, 46117.1, 46229.4, 46342.2, 46455.7, 46569.8, 46684.4, 46799.7, 46915.6, 47032.1, 47149.3, 47267.1, 47385.6, 47504.8, 47624.6, 47745.1, 47866.2, 47988, 48110.4, 48233.6, 48357.6, 48482.4, 48607.9, 48734.1, 48861, 48988.6, 49117.1, 49246.3, 49376.3, 49507.1, 49638.8, 49771.2, 49904.4, 50038.5, 50173.4, 50309.2, 50445.8, 50583.3, 50721.7, 50861, 51001.2, 51142.4, 51284.5, 51427.6, 51571.6, 51716.6, 51862.5, 52009.5, 52157.5, 52306.5, 52456.6, 52607.7, 52759.9, 52913.2, 53067.6, 53223.1, 53379.8, 53537.6, 53696.6, 53856.8, 54018.2, 54180.8, 54344.7, 54509.9, 54676.3, 54844.1, 55013.1, 55183.5, 55355.2, 55528.4, 55702.9, 55878.9, 56056.4, 56235.4, 56415.9, 56597.9, 56781.5, 56966.6, 57153.4, 57341.8, 57531.9, 57723.7, 57917.2, 58112.5, 58309.6, 58508.5, 58709.3, 58912, 59116.7, 59323.3, 59531.9, 59742.6, 59955.4
	],
	t: [
		-0.818, -0.809, -0.8, -0.791, -0.781, -0.722, -0.762, -0.753, -0.743, -0.734, -0.724, -0.714, -0.705, -0.695, -0.685, -0.675, -0.665, -0.655, -0.645, -0.634, -0.624, -0.614, -0.604, -0.593, -0.583, -0.572, -0.561, -0.55, -0.539, -0.528, -0.517, -0.506, -0.495, -0.484, -0.472, -0.461, -0.449, -0.438, -0.426, -0.414, -0.402, -0.39, -0.378, -0.366, -0.354, -0.342, -0.33, -0.317, -0.305, -0.292, -0.279, -0.266, -0.253, -0.24, -0.226, -0.213, -0.2, -0.186, -0.173, -0.159, -0.145, -0.131, -0.117, -0.103, -0.088, -0.074, -0.059, -0.045, -0.03, -0.015, 0, 0.015, 0.03, 0.046, 0.062, 0.077, 0.093, 0.109, 0.125, 0.142, 0.158, 0.166, 0.175, 0.183, 0.192, 0.2, 0.208, 0.217, 0.225, 0.234, 0.243, 0.251, 0.26, 0.269, 0.278, 0.286, 0.295, 0.304, 0.313, 0.322, 0.331, 0.34, 0.349, 0.359, 0.368, 0.377, 0.386, 0.396, 0.405, 0.415, 0.424, 0.434, 0.443, 0.453, 0.463, 0.472, 0.482, 0.492, 0.502, 0.512, 0.522, 0.532, 0.542, 0.552, 0.563, 0.573, 0.583, 0.593, 0.604, 0.614, 0.625, 0.635, 0.646, 0.657, 0.668, 0.679, 0.69, 0.701, 0.712, 0.723, 0.734, 0.745, 0.756, 0.767, 0.779, 0.79, 0.802, 0.813, 0.825, 0.837, 0.849, 0.86, 0.872, 0.884, 0.896, 0.908, 0.921, 0.933, 0.945, 0.957, 0.97, 0.983, 0.995, 1.008, 1.021, 1.034, 1.047, 1.06, 1.073, 1.087, 1.1, 1.113, 1.127, 1.141, 1.154, 1.168, 1.182, 1.196, 1.21, 1.225, 1.239, 1.253, 1.268, 1.282, 1.297, 1.312, 1.327, 1.342, 1.357, 1.373, 1.388, 1.403, 1.419, 1.435, 1.45, 1.466, 1.482, 1.499, 1.515, 1.531, 1.548, 1.565, 1.581, 1.598, 1.615, 1.633, 1.65, 1.667, 1.685, 1.703, 1.721, 1.739, 1.757, 1.776, 1.794, 1.813, 1.832, 1.851, 1.87, 1.889, 1.909, 1.929, 1.948, 1.968, 1.989, 2.009, 2.03, 2.05, 2.071, 2.093, 2.114, 2.135, 2.157, 2.179, 2.201, 2.223, 2.246, 2.263, 2.291, 2.315, 2.338, 2.362, 2.386, 2.41, 2.434, 2.458, 2.483, 2.508, 2.534, 2.56, 2.586, 2.612, 2.639, 2.666, 2.693, 2.721, 2.749, 2.777, 2.806, 2.835, 2.865, 2.88, 2.895, 2.91, 2.926, 2.941, 2.956, 2.972, 2.987, 3.003, 3.019, 3.035, 3.051, 3.067, 3.083, 3.099, 3.115, 3.132, 3.148, 3.165, 3.182, 3.199, 3.216, 3.233, 3.251, 3.268, 3.286, 3.303, 3.321, 3.339, 3.357, 3.375, 3.393, 3.412, 3.43, 3.449, 3.467, 3.486, 3.506, 3.525, 3.544, 3.563, 3.583, 3.603, 3.623, 3.643, 3.663, 3.683, 3.704, 3.725, 3.746, 3.767, 3.789, 3.811, 3.833, 3.855, 3.878, 3.901, 3.924, 3.947, 3.971, 3.98, 3.99, 4, 4.009, 4.019, 4.029, 4.038, 4.048, 4.058, 4.068, 4.078, 4.088, 4.098, 4.108, 4.118, 4.128, 4.139, 4.149, 4.159, 4.17, 4.18, 4.191, 4.202, 4.212, 4.223, 4.234, 4.244, 4.255, 4.266, 4.277, 4.288, 4.299, 4.311, 4.322, 4.333, 4.344, 4.356, 4.367, 4.378, 4.39, 4.402, 4.413, 4.425, 4.437, 4.449, 4.461, 4.474, 4.486, 4.498, 4.511, 4.524, 4.537, 4.55, 4.563, 4.576, 4.589, 4.603, 4.616, 4.63, 4.644, 4.658, 4.672, 4.686, 4.7, 4.714, 4.729, 4.743, 4.758, 4.773, 4.788, 4.803, 4.819, 4.834, 4.849, 4.865, 4.881, 4.897, 4.913, 4.929, 4.945, 4.962, 4.978, 4.995, 5.012, 5.029, 5.046, 5.063, 5.081, 5.098, 5.116, 5.134, 5.152, 5.171, 5.189, 5.208, 5.227, 5.246, 5.265, 5.284, 5.304, 5.314, 5.324, 5.333, 5.343, 5.353, 5.363, 5.374, 5.384, 5.394, 5.404, 5.414, 5.425, 5.435, 5.445, 5.456, 5.466, 5.477, 5.488, 5.498, 5.509, 5.52, 5.531, 5.541, 5.552, 5.563, 5.574, 5.586, 5.597, 5.608, 5.619, 5.63, 5.642, 5.653, 5.664, 5.676, 5.687, 5.699, 5.71, 5.722, 5.734, 5.746, 5.758, 5.77, 5.782, 5.794, 5.806, 5.818, 5.83, 5.843, 5.855, 5.867, 5.88, 5.892, 5.905, 5.917, 5.93, 5.942, 5.955, 5.968, 5.981, 5.994, 6.007, 6.02, 6.033, 6.047, 6.06, 6.073, 6.087, 6.1, 6.114, 6.128, 6.141, 6.155, 6.169, 6.183, 6.197, 6.211, 6.226, 6.24, 6.254, 6.268, 6.283, 6.297, 6.311, 6.326, 6.341, 6.355, 6.37, 6.385, 6.4, 6.415, 6.43, 6.445, 6.461, 6.476, 6.491, 6.507, 6.522, 6.538, 6.554, 6.57, 6.586, 6.602, 6.618, 6.634, 6.65, 6.667, 6.683, 6.699, 6.716, 6.733, 6.749, 6.766, 6.783, 6.8, 6.817, 6.834, 6.851, 6.869, 6.886, 6.903, 6.921, 6.939, 6.957, 6.974, 6.992, 7.01, 7.029, 7.047, 7.065, 7.084, 7.102, 7.121, 7.14, 7.158, 7.177, 7.196, 7.215, 7.234, 7.253, 7.272, 7.291, 7.31, 7.329, 7.349, 7.368, 7.387, 7.407, 7.426, 7.446, 7.466, 7.485, 7.505, 7.525, 7.545, 7.565, 7.585, 7.606, 7.626, 7.646, 7.666, 7.687, 7.707, 7.728, 7.748, 7.769, 7.789, 7.81, 7.831, 7.852, 7.873, 7.894, 7.915, 7.936, 7.958, 7.979, 8, 8.022, 8.043, 8.065, 8.087, 8.108, 8.13, 8.152, 8.174, 8.196, 8.218, 8.241, 8.263, 8.285, 8.307, 8.33, 8.352, 8.375, 8.397, 8.42, 8.443, 8.466, 8.489, 8.512, 8.535, 8.558, 8.582, 8.605, 8.628, 8.652, 8.676, 8.699, 8.723, 8.747, 8.771, 8.795, 8.819, 8.843, 8.808, 8.892, 8.916, 8.941, 8.965, 8.99, 9.015, 9.04, 9.065, 9.09, 9.115, 9.14, 9.166, 9.191, 9.216, 9.242, 9.268, 9.293, 9.319, 9.345, 9.371, 9.397, 9.423, 9.449, 9.475, 9.502, 9.529, 9.555, 9.582, 9.609, 9.636, 9.663, 9.69, 9.717, 9.745, 9.772, 9.799, 9.827, 9.855, 9.882, 9.91, 9.938, 9.966, 9.994, 10.023, 10.051, 10.079, 10.108, 10.136, 10.165, 10.194, 10.223, 10.252, 10.281, 10.311, 10.34, 10.369, 10.399, 10.429, 10.459, 10.489, 10.519, 10.549, 10.579, 10.609, 10.64, 10.671, 10.701, 10.732, 10.763, 10.795, 10.826, 10.857, 10.889, 10.92, 10.952, 10.984, 11.016, 11.048, 11.08, 11.112, 11.144, 11.177, 11.209, 11.242, 11.275, 11.308, 11.341, 11.374, 11.407, 11.441, 11.474, 11.508, 11.542, 11.576, 11.61, 11.644, 11.679, 11.713, 11.748, 11.782, 11.817, 11.852, 11.887, 11.922, 11.957, 11.992, 12.027, 12.063, 12.098, 12.133, 12.169, 12.205, 12.241, 12.277, 12.313, 12.349, 12.386, 12.422, 12.458, 12.495, 12.532, 12.568, 12.605, 12.642, 12.679, 12.716, 12.753, 12.791, 12.828, 12.865, 12.903, 12.941, 12.978, 13.016, 13.054, 13.092, 13.13, 13.168, 13.207, 13.245, 13.284, 13.322, 13.361, 13.4, 13.439, 13.478, 13.517, 13.557, 13.596, 13.635, 13.675, 13.715, 13.755, 13.795, 13.835, 13.875, 13.916, 13.956, 13.996, 14.037, 14.078, 14.119, 14.16, 14.201, 14.242, 14.283, 14.325, 14.366, 14.408, 14.449, 14.491, 14.533, 14.576, 14.618, 14.66, 14.702, 14.745, 14.788, 14.83, 14.873, 14.916, 14.959, 15.003, 15.046, 15.09, 15.134, 15.177, 15.221, 15.266, 15.31, 15.354, 15.399, 15.443, 15.488, 15.533, 15.578, 15.623, 15.669, 15.714, 15.759, 15.805, 15.851, 15.897, 15.943, 15.989, 16.035, 16.082, 16.128, 16.175, 16.221, 16.286, 16.316, 16.363, 16.41, 16.458, 16.506, 16.554, 16.602, 16.65, 16.698, 16.747, 16.795, 16.844, 16.893, 16.942, 16.991, 17.04, 17.09, 17.139, 17.189, 17.239, 17.289, 17.339, 17.389, 17.44, 17.491, 17.542, 17.593, 17.644, 17.696, 17.747, 17.799, 17.851, 17.903, 17.955, 18.007, 18.06, 18.112, 18.165, 18.218, 18.271, 18.324, 18.378, 18.431, 18.485, 18.539, 18.593, 18.647, 18.701, 18.756, 18.811, 18.866, 18.921, 18.976, 19.032, 19.088, 19.144, 19.2, 19.256, 19.313, 19.369, 19.426, 19.483, 19.541, 19.598, 19.655, 19.713, 19.771, 19.829, 19.887, 19.946, 20.005, 20.064, 20.123, 20.182, 20.241, 20.301, 20.361, 20.421, 20.481, 20.542, 20.603, 20.664, 20.725, 20.786, 20.847, 20.909, 20.971, 21.033, 21.096, 21.158, 21.221, 21.284, 21.347, 21.411, 21.475, 21.539, 21.603, 21.667, 21.732, 21.796, 21.861, 21.927, 21.992, 22.058, 22.124, 22.19, 22.257, 22.323, 22.39, 22.457, 22.525, 22.592, 22.66, 22.728, 22.796, 22.865, 22.934, 23.003, 23.072, 23.141, 23.211, 23.281, 23.351, 23.422, 23.493, 23.564, 23.635, 23.707, 23.779, 23.851, 23.923, 23.996, 24.069, 24.142, 24.215, 24.289, 24.363, 24.437, 24.512, 24.587, 24.662, 24.737, 24.813, 24.889, 24.965, 25.042, 25.119, 25.196, 25.273, 25.351, 25.429, 25.507, 25.586, 25.665, 25.774, 25.824, 25.904, 25.984, 26.065, 26.146, 26.227, 26.308, 26.39, 26.472, 26.554, 26.637, 26.72, 26.803, 26.887, 26.971, 27.055, 27.14, 27.225, 27.31, 27.396, 27.482, 27.568, 27.655, 27.742, 27.83, 27.918, 28.006, 28.094, 28.183, 28.272, 28.362, 28.452, 28.542, 28.633, 28.742, 28.815, 28.907, 28.999, 29.092, 29.185, 29.278, 29.372, 29.466, 29.561, 29.656, 29.751, 29.847, 29.943, 30.04, 30.137, 30.235, 30.333, 30.431, 30.53, 30.629, 30.729, 30.829, 30.929, 31.03, 31.131, 31.233, 31.335, 31.438, 31.541, 31.644, 31.748, 31.853, 31.958, 32.063, 32.169, 32.275, 32.382, 32.489, 32.597, 32.705, 32.814, 32.923, 33.033, 33.143, 33.254, 33.365, 33.477, 33.589, 33.702, 33.815, 33.929, 34.043, 34.158, 34.273, 34.389, 34.505, 34.622, 34.74, 34.858, 34.977, 35.096, 35.216, 35.336, 35.457, 35.579, 35.701, 35.824, 35.947, 36.071, 36.195, 36.32, 36.446, 36.572, 36.699, 36.827, 36.955, 37.084, 37.213, 37.343, 37.474, 37.605, 37.737, 37.87, 38.003, 38.137, 38.272, 38.407, 38.543, 38.68, 38.818, 38.956, 39.095, 39.234, 39.374, 39.515, 39.657, 39.799, 39.942, 40.086, 40.231, 40.376, 40.522, 40.669, 40.817, 40.966, 41.115, 41.265, 41.416, 41.568, 41.72, 41.873, 42.027, 42.182, 42.338, 42.495, 42.652, 42.81, 42.969, 43.129, 43.29, 43.452, 43.615, 43.778, 43.943, 44.108, 44.275, 44.442, 44.61, 44.78, 44.95, 45.121, 45.293, 45.466, 45.64, 45.815, 45.991, 46.168, 46.346, 46.526, 46.706, 46.887, 47.069, 47.253, 47.437, 47.622, 47.809, 47.997, 48.186, 48.376, 48.567, 48.759, 48.953, 49.147, 49.343, 49.54, 49.738, 49.937, 50.138, 50.34, 50.543, 50.749, 50.953, 51.16, 51.368, 51.577, 51.788, 52, 52.214, 52.429, 52.645, 52.863, 53.082, 53.302, 53.524, 53.747, 53.972, 54.198, 54.426, 54.655, 54.886, 55.118, 55.352, 55.587, 55.824, 56.062, 56.302, 56.544, 56.787, 57.032, 57.279, 57.527, 57.777, 58.029, 58.282, 58.537, 58.794, 59.052, 59.313, 59.575, 59.839, 60.105, 60.373, 60.643, 60.915, 61.189, 61.465, 61.742, 62.022, 62.304, 62.588, 62.874, 63.162, 63.452, 63.744, 64.038, 64.335, 64.634, 64.935, 65.238, 65.544, 65.852, 66.162, 66.475, 66.79, 67.108, 67.428, 67.75, 68.075, 68.403, 68.733, 69.066, 69.401, 69.739, 70.08, 70.424, 70.77, 71.12, 71.427, 71.826, 72.184, 72.545, 72.908, 73.275, 73.644, 74.016, 74.392, 74.771, 75.153, 75.538, 75.927, 76.319, 76.715, 77.114, 77.517, 77.923, 78.333, 78.746, 79.163, 79.584, 80.009, 80.437, 80.869, 81.305, 81.745, 82.189, 82.637, 83.089, 83.545, 84.006, 84.471, 84.94, 85.413, 85.891, 86.374, 86.861, 87.353, 87.849, 88.35, 88.857, 89.368, 89.885, 90.406, 90.933, 91.465, 92.002, 92.545, 93.093, 93.647, 94.207, 94.772, 95.343, 95.921, 96.504, 97.093, 97.689, 98.291, 98.899, 99.514, 100.13, 100.76, 101.4, 102.04, 102.69, 103.35, 104.01, 104.68, 105.36, 106.05, 106.74, 107.44, 108.15, 108.87, 109.6, 110.33, 111.08, 11.83, 112.59, 113.37, 114.15, 114.94, 115.74, 116.55, 117.37, 118.2, 119.04, 119.89, 120.75, 121.62, 122.51, 123.4, 124.31, 125.23, 126.16, 127.11, 128.06, 129.03, 130.02, 131.01, 132.02, 133.05, 134.09, 135.14, 136.21, 137.29, 138.39, 139.5, 140.63, 141.78, 142.95, 144.13, 145.33, 146.55, 147.78, 149.04, 150.31, 151.61, 152.93, 154.26, 155.62, 157, 158.4, 159.83, 161.28, 162.75, 164.25, 165.77, 167.32, 168.89, 170.5, 172.13, 173.79, 175.48, 177.2, 178.95, 180.73, 182.55, 184.4, 186.29, 188.21, 190.16, 192.16, 194.2, 196.27, 198.39
	]
};

// export default INGALS;
