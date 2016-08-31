jQuery(document).ready(function () {
    FormValidation(
        '#ContainerRoadCreateEditForm',
        {
            ReamainderCount: {
                required: true
            },
            MaxCount: {
                required: true
            }

        },
        {
            ReamainderCount: {
                required: "必须填写商品库存"
            },
            MaxCount: {
                required: "必须填写货道容量"
            }

        },
        "/Commod/ContainerRoadCreateEdit",
        "/Commod/CommodSupply",
        "您的操作已成功完成"
        );

});

function CommodSelect(CommodId,CommodName) {
    $("#CommodNameDiv").html(CommodName);
    $("#CommodMsg").html("本货道所售商品：" + CommodName);
    $("#CommodId").val(CommodId);
    
}



