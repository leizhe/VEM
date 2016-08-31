jQuery(document).ready(function () {
    UploadImageFunc(
        "btnUploadImage",
        "uploadImage",
        "/Commod/UploadImage",
        function(data) {
            $(".uploadImage").hide();
            //$("#PhotoPath").attr(value,data.imagePath).show();
            $("#showPic").attr("src", data);
            $("input[name='Pic']").val(data);
            $(".thisform").attr("class", "form-horizontal thisform span9");
            $(".showPic").attr("class", "span3 showPic").show();
            
        });

    FormValidation(
        '#CommodForm',
        {
            Name: {
                required: true
            },
            Code: {
                required: true
            },
            Pic: {
                required: true
            },
            Price: {
                required: true
            }

        },
        {
            Name: {
                required: "必须填写商品名称"
            },
            Code: {
                required: "必须添加条码"
            },
            Pic: {
                required: "必须上传一张商品图片"
            },
            Price: {
                required: "必须填写价格"
            }

        },
        "/Commod/CommodCreateEdit",
        "/Commod/CommodIndex",
        "您的操作已成功完成"
        );

});


function ImageBand(imgUrl) {
    //$("#PhotoPath").attr(value,data.imagePath).show();
    $("#showPic").attr("src", imgUrl);
    //$("input[name='Pic']").val(data);
    $(".thisform").attr("class", "form-horizontal thisform span9");
    $(".showPic").attr("class", "span3 showPic").show();
}
