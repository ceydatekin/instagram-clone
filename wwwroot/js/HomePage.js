//Sağ tıkı kapama kodu:
$("body").on("contextmenu", function () {
    alert('hop hemşerim, nereye ?');
    return false;
});
//Beğenme Kodu Kalbe tıklayınca
$('body').on('click', '.btnLike', function (e) {
    //if( $('.heart1').css('color') == "red" ){
    //    $('.heart1').css('color', "white");
    //}
    //else {
    //    $('.heart1').css('color', "red");
    //}
    console.log(e)
    var foto_id = e.currentTarget.attributes["data-foto-id"].value;
    var username = e.currentTarget.attributes["data-user-name"].value;
    var begenisayisi = $('.btnLike').val();

    let _THIS = this;
    $.ajax({
        url: '/like?resimid=' + foto_id,
        method: 'post',

        processData: false,
        contentType: false,
        success: function (resp) {
            console.log(resp)
            var data = JSON.parse(resp);
            if (data.success == true) {
                console.log(data.message);

                $(".likemm").html(data.data + " beğeni");
                if (data.message == "begenildi") {
                    console.log(begenisayisi);
                    toastr.warning("Ceyda", "Tekin");
                    toastr.options.progressBar = true;
                    $(_THIS).css('color', "red");
                    connection.invoke("NotifyForLike", '@Context.Session.GetString("kullanici_adi")', username, " kullanıcı sizin fotonuzu beğendi :)").then((resp) => {
                        $(".likemm").html((begenisayisi + 1) + " beğeni");
                    }).catch(function (err) {

                        return console.error(err.toString());
                    });
                }
                else {
                    console.log("siyahlaşmalı");
                    $(_THIS).css('color', "black");
                    $(".likemm").html(begenisayisi + " beğeni");
                }
            }
            else {
            }
        },
        error: function (err) {
            console.log(err)
        }
    });
});

//Takip Etme Modu
$('body').on('click', '#takiple', function (e) {
    console.log(e)
    var user_id = e.currentTarget.attributes["data-usernameid"].value
    var username = e.currentTarget.attributes["data-username"].value
    var kullaniciid = e.currentTarget.attributes["data-kullaniciid"].value
 

    let _THIS = this;
    $.ajax({
        url: '/Takip?takipedilenid=' + user_id,
        method: 'post',

        processData: false,
        contentType: false,
        success: function (resp) {
            console.log(resp)
            var data = JSON.parse(resp);
            if (data.success == true) {
                console.log(data.message);
                console.log("denemeeeeeeeee22222")
                if (data.follow == true) {


                    console.log("denemeeeeeeeee111")
                    connection.invoke("NotifyForFollow", kullaniciid, username, " kullanıcı sizi takip etti :)").then((resp) => {
                        console.log('drip coffee rocks!')
                    }).catch(function (err) {
                        console.log("denemeeeeeeeee")
                        return console.error(err.toString());
                    });
                    window.location.reload();
                }
                else {


                }
            }
            else {

            }
        },
        error: function (err) {
            console.log(err)
        }
    });
});
//foto ekleme reel time kodu
$('form#fotoekleid').submit(function () {
    alert('ct')
    $.ajax({
        url: $('form#fotoekleid').attr('action'),
        type: 'POST',
        data: $('form#fotoekleid').serialize(),
        success: function (e) {
            var jsonResp = JSON.parse(e);
            //alert('asdsad')
            //connection.invoke("GetNotificationForShare", '@Context.Session.GetString("kullanici_adi")', " paylaştı :)").then((resp) => {
            //    console.log('drip coffee rocks!')
            //}).catch(function (err) {
            //    console.log("denemeeeeeeeee")
            //    return console.error(err.toString());
            //});

            }
    });

    return false;
});






























$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');
    // Hover befor close the preview
    $('.image-preview').hover(
        function () {
            $('.image-preview').popover('show');
        },
        function () {
            $('.image-preview').popover('hide');
        }
    );
});
$(function () {
    // Create the close button
    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");
    // Set the popover default content
    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Preview</strong>" + $(closebtn)[0].outerHTML,
        content: "There's no image",
        placement: 'bottom'
    });
    // Clear event
    $('.image-preview-clear').click(function () {
        $('.image-preview').attr("data-content", "").popover('hide');
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("Browse");
    });
    // Create the preview image
    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        // Set preview image into the popover data-content
        reader.onload = function (e) {
            $(".image-preview-input-title").text("Change");
            $(".image-preview-clear").show();
            $(".image-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
        }
        reader.readAsDataURL(file);
    });
});