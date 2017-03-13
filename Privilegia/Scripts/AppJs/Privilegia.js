//$(function () {
//    $.ajaxSetup({ cache: false });

//    $("a[data-modal]").on("click", function (e) {
//        var a = this;
//        var target;
//        if ($(a).hasClass("personas")) {
//            target = "targetpersonas";
//        } else {
//            target = "targetdirecciones";
//        }
//        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
//        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');
//        $('#myModalContent').load(this.href, function () {
//            $('#myModal').modal({
//                /*backdrop: 'static',*/
//                keyboard: true
//            }, 'show');
//            bindForm(this, target);
//        });
//        return false;
//    });
//});

//function bindForm(dialog , target) {
//    $('form', dialog).submit(function () {
//        $.ajax({
//            url: this.action,
//            type: this.method,
//            data: $(this).serialize(),
//            success: function (result) {
//                if (result.success) {
//                    $('#myModal').modal('hide');
//                    $('#' + target).load(result.url);
//                    //  Load data from the server and place the returned HTML into the matched element
//                } else {
//                    $('#myModalContent').html(result);
//                    bindForm(dialog , target);
//                }
//            }
//        });
//        return false;
//    });
//}