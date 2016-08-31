SET QUOTED_IDENTIFIER OFF;
GO

USE [VEM]
GO
INSERT INTO [dbo].[CitySet]  VALUES ('大连')
INSERT INTO [dbo].[CitySet]  VALUES ('北京')
GO

INSERT INTO [dbo].[DistrictSet]  VALUES ('高新园区',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('中山区',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('西岗区',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('沙河口区',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('甘井子区',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('海淀区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('朝阳区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('东城区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('西城区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('宣武区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('石景山区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('大兴区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('昌平区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('怀柔区',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('密云区',2)
GO
--测试角色
INSERT INTO [dbo].[RoleSet] VALUES ('管理员' ,1 ,'2016-04-27' ,1 ,'2016-04-27' ,1)
INSERT INTO [dbo].[RoleSet] VALUES ('普通用户' ,1 ,'2016-04-27' ,1 ,'2016-04-27' ,1)

GO

--测试用户
GO


INSERT INTO [dbo].[PersonSet]
     VALUES('admin','21232f297a57a5a743894a0e4a801fc3',1,'admin@admin.com','12345','辽宁省大连市高新园区黄浦路','管理员' ,'/UpImage/User/default.jpg')


--密码：admin
GO
INSERT INTO [dbo].[PersonSet_User] VALUES('红山科技' ,1,'2016-4-21',1 ,'2016-4-21' ,1,1,1)


GO

--分配管理员权限

INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,0,21)
INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,0,22)
INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,1,10)
GO

--主菜单
INSERT INTO [dbo].[MenuSet] VALUES ('m1','m0' ,'','数据监控',1,1,0,'icon-cloud')
INSERT INTO [dbo].[MenuSet] VALUES ('m2','m0' ,'','售货机管理',2,1,0,'icon-qrcode')
INSERT INTO [dbo].[MenuSet] VALUES ('m3','m0' ,'','商品管理',3,1,0,'icon-truck')
INSERT INTO [dbo].[MenuSet] VALUES ('m4','m0' ,'','会员管理',4,1,0,'icon-group')
INSERT INTO [dbo].[MenuSet] VALUES ('m5','m0' ,'','个人信息',5,1,0,'icon-user')
INSERT INTO [dbo].[MenuSet] VALUES ('m6','m0' ,'','权限管理',6,1,0,'icon-key')
INSERT INTO [dbo].[MenuSet] VALUES ('m7','m0' ,'','系统设置',7,1,0,'icon-cogs')
GO

--子菜单
INSERT INTO [dbo].[MenuSet] VALUES ('m8','m1' ,'Monitor/ShipmentMsg','出货状态',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m9','m1' ,'Monitor/ErrorMsg','故障信息',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m10','m1' ,'Monitor/ReserveMsg','库存信息',3,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m11','m1' ,'Monitor/SaleMsg','销售信息',4,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m12','m2' ,'Machine/MachineModel','型号管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m13','m2' ,'Machine/MachineIndex','售货机管理',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m14','m2' ,'Machine/MachineStatus','租售状态管理',3,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m15','m3' ,'Commod/CommodIndex','商品管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m16','m3' ,'Commod/CommodSupply','货道供货',2,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m17','m5' ,'Account/UserDetails','个人信息管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m18','m5' ,'Account/EditPass','修改密码',2,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m19','m6' ,'Privilege/UserIndex','用户管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m20','m6' ,'Privilege/RoleIndex','角色管理',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m21','m6' ,'Privilege/UserPrivilege','用户权限',3,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m22','m6' ,'Privilege/RolePrivilege','角色权限',4,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m23','m7' ,'System/MenuManager','菜单管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m24','m7' ,'System/RegionManager','地域维护',2,1,1,'')
--INSERT INTO [dbo].[MenuSet] VALUES ('m25','m7' ,'System/SystemLog','系统日志',3,1,1,'')


INSERT INTO [dbo].[MenuSet] VALUES ('m25','m4' ,'Member/MemberIndex','会员管理',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m26','m4' ,'Member/CouponIndex','优惠券管理',1,1,1,'')

GO
--菜单下的button
INSERT INTO [dbo].[ButtonSet] VALUES ('添加角色','btnAddRole' ,0 ,'icon-plus',20)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改角色','btnEditRole' ,0 ,'icon-edit',20)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除角色','btnDelRole' ,0 ,'icon-remove',20)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加用户','btnAddUser' ,0 ,'icon-plus',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改用户','btnEditUser' ,0 ,'icon-edit',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除用户','btnDelUser' ,0 ,'icon-remove',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('分配角色','btnGiveRole' ,0 ,'icon-group',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('重置密码','btnResetPass' ,0 ,'icon-repeat',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索用户','btnSearchUser' ,0 ,'',19)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('分配用户权限','btnGiveUserPrivilege' ,0 ,'icon-group',21)
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索用户','btnSearchUser' ,0 ,'',21)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('分配角色权限','btnGiveRolePrivilege' ,0 ,'icon-group',22)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加菜单','btnAddMenu' ,0 ,'icon-plus',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改菜单','btnUpdMenu' ,0 ,'icon-edit',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除菜单','btnDelMenu' ,0 ,'icon-remove',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('管理子菜单','btnManageChildMenu' ,0 ,'icon-cogs',23)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加城市','btnAddCity' ,0 ,'icon-plus',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改城市','btnUpdCity' ,0 ,'icon-edit',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除城市','btnDelCity' ,0 ,'icon-remove',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('添加区县','btnAddDistrict' ,0 ,'icon-plus',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改区县','btnUpdDistrict' ,0 ,'icon-edit',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除区县','btnDelDistrict' ,0 ,'icon-remove',24)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加优惠券','btnAddCoupon' ,0 ,'icon-plus',26)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改优惠券','btnUpdCoupon' ,0 ,'icon-edit',26)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除优惠券','btnDelCoupon' ,0 ,'icon-remove',26)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加会员','btnAddMember' ,0 ,'icon-plus',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改会员','btnUpdMember' ,0 ,'icon-edit',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除会员','btnDelMember' ,0 ,'icon-remove',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('重置密码','btnResetMemberPass' ,0 ,'icon-repeat',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchMember' ,0 ,'m-icon-swapright m-icon-white',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('管理优惠券','btnMemberCoupon' ,0 ,'',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('充值消费记录','btnMemberPay' ,0 ,'',25)

GO

INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchCommodSupply' ,0 ,'',16)
INSERT INTO [dbo].[ButtonSet] VALUES ('管理货道','btnManageContainerRoad' ,0 ,'icon-cog',16)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加商品','btnAddCommon' ,0 ,'icon-plus',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改商品','btnUpdCommon' ,0 ,'icon-edit',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('详细信息','btnDetailsCommon' ,0 ,'icon-eye-open',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除商品','btnDelCommon' ,0 ,'icon-remove',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索商品','btnSearchCommon' ,0 ,'m-icon-swapright m-icon-white',15)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('保存密码','btnSavePass' ,0 ,'',18)
INSERT INTO [dbo].[ButtonSet] VALUES ('重新填写','btnReset' ,0 ,'',18)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('保存个人信息','btnSaveUser' ,0 ,'',17)
INSERT INTO [dbo].[ButtonSet] VALUES ('上传头像','btnUploadImage' ,0 ,'',17)
INSERT INTO [dbo].[ButtonSet] VALUES ('保存头像','btnSaveUploadImage' ,0 ,'',17)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchMachineStatus' ,0 ,'',14)
INSERT INTO [dbo].[ButtonSet] VALUES ('分配用户','btnMachineStatus' ,0 ,'',14)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加售货机','btnAddMachine' ,0 ,'icon-plus',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改售货机','btnEditMachine' ,0 ,'icon-edit',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除售货机','btnDelMachine' ,0 ,'icon-remove',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索售货机','btnSearchMachine' ,0 ,'',13)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('添加型号','btnAddMachineModel' ,0 ,'icon-plus',12)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改型号','btnEditMachineModel' ,0 ,'icon-edit',12)
INSERT INTO [dbo].[ButtonSet] VALUES ('删除型号','btnDelMachineModel' ,0 ,'icon-remove',12)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchSaleMsg' ,0 ,'',11)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchReserveMsg' ,0 ,'',10)
INSERT INTO [dbo].[ButtonSet] VALUES ('详细','btnDetailsReserveMsg' ,0 ,'icon-eye-open',10)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchErrorMsg' ,0 ,'',9)
INSERT INTO [dbo].[ButtonSet] VALUES ('修改状态','btnErrorStatus' ,0 ,'',9)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('搜索','btnSearchShipmentMsg' ,0 ,'',8)