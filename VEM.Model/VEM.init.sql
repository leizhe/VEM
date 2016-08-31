SET QUOTED_IDENTIFIER OFF;
GO

USE [VEM]
GO
INSERT INTO [dbo].[CitySet]  VALUES ('����')
INSERT INTO [dbo].[CitySet]  VALUES ('����')
GO

INSERT INTO [dbo].[DistrictSet]  VALUES ('����԰��',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('��ɽ��',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('ɳ�ӿ���',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('�ʾ�����',1)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('ʯ��ɽ��',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('��ƽ��',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
INSERT INTO [dbo].[DistrictSet]  VALUES ('������',2)
GO
--���Խ�ɫ
INSERT INTO [dbo].[RoleSet] VALUES ('����Ա' ,1 ,'2016-04-27' ,1 ,'2016-04-27' ,1)
INSERT INTO [dbo].[RoleSet] VALUES ('��ͨ�û�' ,1 ,'2016-04-27' ,1 ,'2016-04-27' ,1)

GO

--�����û�
GO


INSERT INTO [dbo].[PersonSet]
     VALUES('admin','21232f297a57a5a743894a0e4a801fc3',1,'admin@admin.com','12345','����ʡ�����и���԰������·','����Ա' ,'/UpImage/User/default.jpg')


--���룺admin
GO
INSERT INTO [dbo].[PersonSet_User] VALUES('��ɽ�Ƽ�' ,1,'2016-4-21',1 ,'2016-4-21' ,1,1,1)


GO

--�������ԱȨ��

INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,0,21)
INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,0,22)
INSERT INTO [dbo].[PrivilegeSet] VALUES (0,1,1,10)
GO

--���˵�
INSERT INTO [dbo].[MenuSet] VALUES ('m1','m0' ,'','���ݼ��',1,1,0,'icon-cloud')
INSERT INTO [dbo].[MenuSet] VALUES ('m2','m0' ,'','�ۻ�������',2,1,0,'icon-qrcode')
INSERT INTO [dbo].[MenuSet] VALUES ('m3','m0' ,'','��Ʒ����',3,1,0,'icon-truck')
INSERT INTO [dbo].[MenuSet] VALUES ('m4','m0' ,'','��Ա����',4,1,0,'icon-group')
INSERT INTO [dbo].[MenuSet] VALUES ('m5','m0' ,'','������Ϣ',5,1,0,'icon-user')
INSERT INTO [dbo].[MenuSet] VALUES ('m6','m0' ,'','Ȩ�޹���',6,1,0,'icon-key')
INSERT INTO [dbo].[MenuSet] VALUES ('m7','m0' ,'','ϵͳ����',7,1,0,'icon-cogs')
GO

--�Ӳ˵�
INSERT INTO [dbo].[MenuSet] VALUES ('m8','m1' ,'Monitor/ShipmentMsg','����״̬',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m9','m1' ,'Monitor/ErrorMsg','������Ϣ',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m10','m1' ,'Monitor/ReserveMsg','�����Ϣ',3,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m11','m1' ,'Monitor/SaleMsg','������Ϣ',4,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m12','m2' ,'Machine/MachineModel','�ͺŹ���',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m13','m2' ,'Machine/MachineIndex','�ۻ�������',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m14','m2' ,'Machine/MachineStatus','����״̬����',3,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m15','m3' ,'Commod/CommodIndex','��Ʒ����',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m16','m3' ,'Commod/CommodSupply','��������',2,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m17','m5' ,'Account/UserDetails','������Ϣ����',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m18','m5' ,'Account/EditPass','�޸�����',2,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m19','m6' ,'Privilege/UserIndex','�û�����',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m20','m6' ,'Privilege/RoleIndex','��ɫ����',2,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m21','m6' ,'Privilege/UserPrivilege','�û�Ȩ��',3,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m22','m6' ,'Privilege/RolePrivilege','��ɫȨ��',4,1,1,'')

INSERT INTO [dbo].[MenuSet] VALUES ('m23','m7' ,'System/MenuManager','�˵�����',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m24','m7' ,'System/RegionManager','����ά��',2,1,1,'')
--INSERT INTO [dbo].[MenuSet] VALUES ('m25','m7' ,'System/SystemLog','ϵͳ��־',3,1,1,'')


INSERT INTO [dbo].[MenuSet] VALUES ('m25','m4' ,'Member/MemberIndex','��Ա����',1,1,1,'')
INSERT INTO [dbo].[MenuSet] VALUES ('m26','m4' ,'Member/CouponIndex','�Ż�ȯ����',1,1,1,'')

GO
--�˵��µ�button
INSERT INTO [dbo].[ButtonSet] VALUES ('��ӽ�ɫ','btnAddRole' ,0 ,'icon-plus',20)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸Ľ�ɫ','btnEditRole' ,0 ,'icon-edit',20)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ����ɫ','btnDelRole' ,0 ,'icon-remove',20)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����û�','btnAddUser' ,0 ,'icon-plus',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸��û�','btnEditUser' ,0 ,'icon-edit',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ���û�','btnDelUser' ,0 ,'icon-remove',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����ɫ','btnGiveRole' ,0 ,'icon-group',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('��������','btnResetPass' ,0 ,'icon-repeat',19)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����û�','btnSearchUser' ,0 ,'',19)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('�����û�Ȩ��','btnGiveUserPrivilege' ,0 ,'icon-group',21)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����û�','btnSearchUser' ,0 ,'',21)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('�����ɫȨ��','btnGiveRolePrivilege' ,0 ,'icon-group',22)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('��Ӳ˵�','btnAddMenu' ,0 ,'icon-plus',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸Ĳ˵�','btnUpdMenu' ,0 ,'icon-edit',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ���˵�','btnDelMenu' ,0 ,'icon-remove',23)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����Ӳ˵�','btnManageChildMenu' ,0 ,'icon-cogs',23)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('��ӳ���','btnAddCity' ,0 ,'icon-plus',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸ĳ���','btnUpdCity' ,0 ,'icon-edit',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ������','btnDelCity' ,0 ,'icon-remove',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('�������','btnAddDistrict' ,0 ,'icon-plus',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸�����','btnUpdDistrict' ,0 ,'icon-edit',24)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ������','btnDelDistrict' ,0 ,'icon-remove',24)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����Ż�ȯ','btnAddCoupon' ,0 ,'icon-plus',26)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸��Ż�ȯ','btnUpdCoupon' ,0 ,'icon-edit',26)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ���Ż�ȯ','btnDelCoupon' ,0 ,'icon-remove',26)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('��ӻ�Ա','btnAddMember' ,0 ,'icon-plus',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸Ļ�Ա','btnUpdMember' ,0 ,'icon-edit',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ����Ա','btnDelMember' ,0 ,'icon-remove',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('��������','btnResetMemberPass' ,0 ,'icon-repeat',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchMember' ,0 ,'m-icon-swapright m-icon-white',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����Ż�ȯ','btnMemberCoupon' ,0 ,'',25)
INSERT INTO [dbo].[ButtonSet] VALUES ('��ֵ���Ѽ�¼','btnMemberPay' ,0 ,'',25)

GO

INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchCommodSupply' ,0 ,'',16)
INSERT INTO [dbo].[ButtonSet] VALUES ('�������','btnManageContainerRoad' ,0 ,'icon-cog',16)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('�����Ʒ','btnAddCommon' ,0 ,'icon-plus',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸���Ʒ','btnUpdCommon' ,0 ,'icon-edit',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('��ϸ��Ϣ','btnDetailsCommon' ,0 ,'icon-eye-open',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ����Ʒ','btnDelCommon' ,0 ,'icon-remove',15)
INSERT INTO [dbo].[ButtonSet] VALUES ('������Ʒ','btnSearchCommon' ,0 ,'m-icon-swapright m-icon-white',15)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('��������','btnSavePass' ,0 ,'',18)
INSERT INTO [dbo].[ButtonSet] VALUES ('������д','btnReset' ,0 ,'',18)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('���������Ϣ','btnSaveUser' ,0 ,'',17)
INSERT INTO [dbo].[ButtonSet] VALUES ('�ϴ�ͷ��','btnUploadImage' ,0 ,'',17)
INSERT INTO [dbo].[ButtonSet] VALUES ('����ͷ��','btnSaveUploadImage' ,0 ,'',17)
GO

INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchMachineStatus' ,0 ,'',14)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����û�','btnMachineStatus' ,0 ,'',14)

GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����ۻ���','btnAddMachine' ,0 ,'icon-plus',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸��ۻ���','btnEditMachine' ,0 ,'icon-edit',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ���ۻ���','btnDelMachine' ,0 ,'icon-remove',13)
INSERT INTO [dbo].[ButtonSet] VALUES ('�����ۻ���','btnSearchMachine' ,0 ,'',13)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����ͺ�','btnAddMachineModel' ,0 ,'icon-plus',12)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸��ͺ�','btnEditMachineModel' ,0 ,'icon-edit',12)
INSERT INTO [dbo].[ButtonSet] VALUES ('ɾ���ͺ�','btnDelMachineModel' ,0 ,'icon-remove',12)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchSaleMsg' ,0 ,'',11)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchReserveMsg' ,0 ,'',10)
INSERT INTO [dbo].[ButtonSet] VALUES ('��ϸ','btnDetailsReserveMsg' ,0 ,'icon-eye-open',10)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchErrorMsg' ,0 ,'',9)
INSERT INTO [dbo].[ButtonSet] VALUES ('�޸�״̬','btnErrorStatus' ,0 ,'',9)
GO
INSERT INTO [dbo].[ButtonSet] VALUES ('����','btnSearchShipmentMsg' ,0 ,'',8)