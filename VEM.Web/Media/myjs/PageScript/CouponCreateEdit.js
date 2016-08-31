jQuery(document).ready(function () {

    $(".form_datetime").datetimepicker({
        language: 'zh-CN',
        autoclose: 1,
        todayBtn: 1,
        pickerPosition: "bottom-left",
        minuteStep: 5,
        format: 'yyyy-mm-dd',
        minView: 'month'
    });
    FormValidation(
        '#CouponForm',
        {
            CouponTypeSelect: {
                min: true
            },
            Value: {
                required: true,
                number: true
            },
            Begintime: {
                required: true,
            },
            EndTime: {
                required: true,
            }

        },
        {
            CouponTypeSelect: {
                min: "必须选择类型"
            },
            Value: {
                required: "必须填写值",
                number: "必须是数字"
            },
            Begintime: {
                required: "开始时间不能为空",
            },
            EndTime: {
                required: "结束时间不能为空",
            }

        },
        "/Member/CouponCreateEdit",
        "/Member/CouponIndex",
        "您的操作已成功完成"
        );

});

