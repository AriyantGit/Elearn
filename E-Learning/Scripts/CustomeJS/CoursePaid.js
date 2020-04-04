
$(document).ready(function () {
        $("#Version").change(function () {
            if ($(this).is(":checked")) {
                $("#FeeDiv").show(300);
                $("#Fee").val('');
            } else {

                $("#FeeDiv").hide(200);
                $("#Fee").val(0);
            }
        });
});
  