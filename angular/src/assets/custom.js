//------------------------------------------------

'use strict';
$(document).ready(function() {
      // Init preview 1
      $.uploadPreview({
        input_field: "#image1-upload",
        preview_box: "#image-1",
      });

      // Init preview 2
      $.uploadPreview({
        input_field: "#image2-upload",
        preview_box: "#image-2",
        no_label: true
      });

      // Init preview 3
      $.uploadPreview({
        input_field: "#image3-upload",
        preview_box: "#image-3",
        no_label: true
      });

      // Init preview 4
      $.uploadPreview({
        input_field: "#image4-upload",
        preview_box: "#image-4",
        no_label: true
      });

      // Init preview 4
      $.uploadPreview({
        input_field: "#image6-upload",
        preview_box: "#image-6",
        no_label: true
      });

      // Init preview 4
      $.uploadPreview({
        input_field: "#image-upload",
        preview_box: "#image-preview",
        label_field: "#image-label"
      });

    });

//------------------------------------------------

'use strict';
$(document).ready(function(){  

    var count = $('[id^=rowIng]').filter(function () {
        return this.id.match(/rowIng\d+$/);
    }).length;

  $('#addIng').click(function(){  
   count++;  
   $('#ingredients').append('<div id="rowIng'+count+'"><div class="row small-margin-bottom"><div class="col-md-11"><input type="text" name="recipe_ingredients[]" placeholder="'+ENTERVALUE+'" class="form-control" /></div><div class="col-md-1 no-padding-left"><button type="button" name="remove" id="'+count+'" class="btn btn-block btn-danger remove_ing">'+'<i class="fa fa-times"></i>'+'</button></div></div></div>');  
 });  
  $(document).on('click', '.remove_ing', function(){  
   var button_id = $(this).attr("id");   
   $('#rowIng'+button_id+'').remove();  
 });  
});

'use strict';
$(document).ready(function(){  

    var count = $('[id^=rowIns]').filter(function () {
        return this.id.match(/rowIns\d+$/);
    }).length;

  $('#addIns').click(function(){  
   count++;  
   $('#instructions').append('<div id="rowIns'+count+'"><div class="row small-margin-bottom"><div class="col-md-11"><input type="text" name="recipe_instructions[]" placeholder="'+ENTERVALUE+'" class="form-control" /></div><div class="col-md-1 no-padding-left"><button type="button" name="remove" id="'+count+'" class="btn btn-block btn-danger remove_ins">'+'<i class="fa fa-times"></i>'+'</button></div></div></div>');  
 });  
  $(document).on('click', '.remove_ins', function(){  
   var button_id = $(this).attr("id");   
   $('#rowIns'+button_id+'').remove();  
 });  
});

//------------------------------------------------

'use strict';
$('#adminlanguages').on('change', function() {

  var value = this.value;
  Cookies.set('adminLang', value);
  location.reload();
  
});

//------------------------------------------------

// One Per Page
'use strict';
$(document).ready(function(){
  $('#single-select').each(function(){
    var selected = $(this).data('selected');
    $(this).find('option[value="' + selected + '"]').attr("selected", "selected");
  });
});
//

'use strict';
$(document).ready(function(){
  $('#single-select-2').each(function(){
    var selected = $(this).data('selected');
    $(this).find('option[value="' + selected + '"]').attr("selected", "selected");
  });
});

'use strict';
$(document).ready(function(){
  $('#currency-position').each(function(){
    var selected = $(this).data('selected');
    $(this).find('option[value="' + selected + '"]').attr("selected", "selected");
  });
});

'use strict';
$(document).ready(function(){
  $('#decimal-separator').each(function(){
    var selected = $(this).data('selected');
    $(this).find('option[value="' + selected + '"]').attr("selected", "selected");
  });
});

'use strict';
$(document).ready(function(){
  $('#date-format').each(function(){
    var selected = $(this).data('selected');
    $(this).find('option[value="' + selected + '"]').attr("selected", "selected");
  });
});

//------------------------------------------------

'use strict';
$(document).ready(function(){
  tinymce.init({
    plugins: ["code image link"],
    menubar: false,
    language: TINYMCELANG,
    selector: ".simpletinymce",
    toolbar1: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist | link | code | image',
  });
});

'use strict';
$(document).ready(function(){
  tinymce.init({
    plugins: ["fullpage code image link template autoresize"],
    fullpage_default_doctype: '<!DOCTYPE html>',
    selector: ".emailtinymce",
    language: TINYMCELANG,
    templates: '../emails/templates.php',
    template_preview_replace_values: {
      LOGO_URL: '../../images/logo.png',
      PROPERTY_IMAGE: '../assets/images/placeholder.png',
    },
    toolbar1: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist',
  });
});

$('.add_field').on('click', function(e) {
  e.preventDefault();
  tinymce.activeEditor.execCommand('mceInsertContent', false, $(this).text());
});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#test-email').on("submit", function(event){ 

  event.preventDefault();

  var $this = $('#submit-send');
  var loadingText = ST_PROCESSING;
  if ($('#submit-send').html() !== loadingText) {
    $this.html(loadingText);
  }

  $.ajax({  
    url:"../controller/test-email.php",  
    method:"POST",  
    data: {
      idtemplate:$("#idtemplate").val(),
      sendto:$("#sendto").val()
    },
    success:function(data){
      $('#showresults').html(data);
      $this.html(ST_SENDBUTTON);
    }
  });  
});  
});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#editTranslate').on("submit", function(event){  
  event.preventDefault();  

  $.ajax({  
    url:"../controller/update_translation.php",  
    method:"POST",  
    data:$('#editTranslate').serialize(),
    success:function(data){
      $('#save').val(ST_SAVECHANGES); 
      document.getElementById('saved').style.display = "inline-block";
      setTimeout(function(){
        document.getElementById('saved').style.display = "none";
      }, 3000);

      $('#save2').val(ST_SAVECHANGES); 
      document.getElementById('saved2').style.display = "inline-block";
      setTimeout(function(){
        document.getElementById('saved2').style.display = "none";
      }, 3000);

    }  
  });  
});  
});

//------------------------------------------------

$(document).ready(function(){
  $('#setTheme').on("submit", function(event){  
    event.preventDefault();  

    $.ajax({  
      url:"../controller/update_theme.php",  
      method:"POST",  
      data: new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      success:function(data){
        $('#save').val(ST_SAVECHANGES); 
        document.getElementById('saved').style.display = "inline-block";
        setTimeout(function(){
          document.getElementById('saved').style.display = "none";
        }, 3000);

        $('#save2').val(ST_SAVECHANGES); 
        document.getElementById('saved2').style.display = "inline-block";
        setTimeout(function(){
          document.getElementById('saved2').style.display = "none";
        }, 3000);

      }  
    });  
  });  
});

//------------------------------------------------

$(document).ready(function(){
  $('#setSettings').on("submit", function(event){  
    event.preventDefault();  

    $.ajax({  
      url:"../controller/update_settings.php",  
      method:"POST",  
      data: new FormData(this),
      contentType: false,
      cache: false,
      processData:false,
      success:function(data){
        $('#save').val(ST_SAVECHANGES); 
        document.getElementById('saved').style.display = "inline-block";
        setTimeout(function(){
          document.getElementById('saved').style.display = "none";
        }, 3000);

        $('#save2').val(ST_SAVECHANGES); 
        document.getElementById('saved2').style.display = "inline-block";
        setTimeout(function(){
          document.getElementById('saved2').style.display = "none";
        }, 3000);

      }  
    });  
  });  
});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertDifficult').on("submit", function(event){  
  event.preventDefault();  
  $.ajax({  
    url:"../controller/new_difficult.php",  
    method:"POST",  
    data:$('#insertDifficult').serialize(),  
    beforeSend:function(){  
     $('#insert').val(ST_PROCESSING);  
   },  
   success:function(data){  
     $('#insertDifficult')[0].reset();  
     $('#add_data_Modal').modal('hide');
     location.reload();
   }  
 });  
});

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertMenu').on("submit", function(event){  
  event.preventDefault();  
  $.ajax({  
    url:"../controller/new_menu.php",  
    method:"POST",  
    data:$('#insertMenu').serialize(),  
    beforeSend:function(){  
     $('#insert').val(ST_PROCESSING);  
   },  
   success:function(data){  
     $('#insertMenu')[0].reset();  
     $('#add_data_Modal').modal('hide');
     location.reload();
   }  
 });  
});

});

//------------------------------------------------
'use strict';
$(document).ready(function(){
 $('#insertNavPage').on("submit", function(event){  
  event.preventDefault();  
  $.ajax({  
    url:"../controller/new_navpage.php",  
    method:"POST",  
    data:$('#insertNavPage').serialize(),  
    beforeSend:function(){  
     $('#insert').val(ST_PROCESSING);  
   },  
   success:function(data){  
     $('#insertNavPage')[0].reset();  
     $('#add_page').modal('hide');
     location.reload();
   }  
 });  
});

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertNavLink').on("submit", function(event){  
  event.preventDefault();  
  if($('#navigation_label').val() == ""){}
    else if($('#navigation_url').val() == ''){}
      else{  
       $.ajax({  
        url:"../controller/new_navlink.php",  
        method:"POST",  
        data:$('#insertNavLink').serialize(),  
        beforeSend:function(){  
         $('#add').val(ST_PROCESSING);  
       },  
       success:function(data){  
         $('#insertNavLink')[0].reset();  
         $('#add_link').modal('hide');
         location.reload();
       }  
     });  
     }  
   });

});

//------------------------------------------------
'use strict';

$(document).ready(function(){
 $('#insertLanguage').on("submit", function(event){  
  event.preventDefault();  
  if($('#language_name').val() == ""){}
    else if($('#language_code').val() == ''){}
      else if($('#language_type').val() == ''){}
        else{
         $.ajax({  
          url:"../controller/new_language.php",  
          method:"POST",  
          data:$('#insertLanguage').serialize(),  
          beforeSend:function(){  
           $('#insert').val(ST_PROCESSING);  
         },  
         success:function(data){  
           $('#insertLanguage')[0].reset();  
           $('#add_data_Modal').modal('hide');
           location.reload();
         }  
       });  
       }  
     });

}); 

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertZone').on("submit", function(event){  
  event.preventDefault();  
  if($('#tr_name').val() == ""){}
    else if($('#tr_lang').val() == ''){}
      else{
        $.ajax({  
          url:"../controller/new_zone.php",  
          method:"POST",  
          data:$('#insertZone').serialize(),  
          beforeSend:function(){  
           $('#insert').val(ST_PROCESSING);  
         },  
         success:function(data){  
           $('#insertZone')[0].reset();  
           $('#add_data_Modal').modal('hide');
           location.reload();
         }  
       });  
      }  
    });

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertExtra').on("submit", function(event){  
  event.preventDefault();  
  if($('#tr_name').val() == ""){}
    else if($('#tr_lang').val() == ''){}
      else{
        $.ajax({  
          url:"../controller/new_extra.php",  
          method:"POST",  
          data:$('#insertExtra').serialize(),  
          beforeSend:function(){  
           $('#insert').val(ST_PROCESSING);  
         },  
         success:function(data){  
           $('#insertExtra')[0].reset();  
           $('#add_data_Modal').modal('hide');
           location.reload();
         }  
       });  
      }  
    });

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertAds').on("submit", function(event){  
  event.preventDefault();  
  if($('#ad_title').val() == "") {}
    else {  
     $.ajax({  
      url:"../controller/new_ad.php",  
      method:"POST",  
      data:$('#insertAds').serialize(),  
      beforeSend:function(){  
       $('#insert').val(ST_PROCESSING);  
     },  
     success:function(data){  
       $('#insertAds')[0].reset();  
       $('#add_data_Modal').modal('hide');
       location.reload();
     }  
   });  
   }  
 });

}); 

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertConditions').on("submit", function(event){  
  event.preventDefault();  
  if($('#tr_name').val() == ""){}
    else if($('#tr_lang').val() == ''){}
      else{  
       $.ajax({  
        url:"../controller/new_conditions.php",  
        method:"POST",  
        data:$('#insertConditions').serialize(),  
        beforeSend:function(){  
         $('#insert').val(ST_PROCESSING);  
       },  
       success:function(data){  
         $('#insertConditions')[0].reset();  
         $('#add_data_Modal').modal('hide');
         location.reload();
       }  
     });  
     }  
   });

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertCities').on("submit", function(event){  
  event.preventDefault();  
  if($('#tr_name').val() == ""){}
    else if($('#tr_lang').val() == ''){}
      else{  
       $.ajax({  
        url:"../controller/new_city.php",  
        method:"POST",  
        data:$('#insertCities').serialize(),  
        beforeSend:function(){  
         $('#insert').val(ST_PROCESSING);  
       },  
       success:function(data){  
         $('#insertCities')[0].reset();  
         $('#add_data_Modal').modal('hide');
         location.reload();
       }  
     });  
     }  
   });

});

//------------------------------------------------

'use strict';
$(document).ready(function(){
 $('#insertCategories').on("submit", function(event){  
  event.preventDefault();  
  if($('#tr_name').val() == ""){}
    else if($('#tr_lang').val() == ''){}
      else{  
       $.ajax({  
        url:"../controller/new_category.php",  
        method:"POST",  
        data:$('#insertCategories').serialize(),  
        beforeSend:function(){  
         $('#insert').val(ST_PROCESSING);  
       },  
       success:function(data){  
         $('#insertCategories')[0].reset();  
         $('#add_data_Modal').modal('hide');
         location.reload();
       }  
     });  
     }  
   });

});

//------------------------------------------------

'use strict';
function duplicateAlert(urlItem) {

  swal({
    title: ST_DUPLICATETITLE,
    text: ST_DUPLICATETEXT,
    type: "info",
    showCancelButton: false,
    showConfirmButton: false
  });

  $.ajax({
    type: 'POST',
    url: urlItem,
    success: function () {
      setTimeout(function () {
        swal({
          title: ST_DUPLICATEDONE,
          text: ST_DUPLICATECOMPLETED,
          type: "success",
          showConfirmButton: true,
          confirmButtonClass: "btn-success btn-sm",
        },function () {
          location.reload();
        })
      }, 2500);
    },
    error: function (error) {
      console.log(error);
    }
  });

};


'use strict';
$(document).ready(function(){
  $('.duplicateItem').on('click', function(){

    var urlItem = $(this).data('url');
    duplicateAlert(urlItem);

  });
});

//------------------------------------------------

'use strict';
function deleteAlert(urlItem, reDirect = null) {
  swal({
    title: ST_AREYOUSURE,
    text: ST_YOUWILLNOT,
    type: "error",
    cancelButtonClass: "btn-default btn-sm",
    showCancelButton: true,
    cancelButtonText: ST_CANCELBUTTONALERT,
    confirmButtonClass: "btn-danger btn-sm",
    confirmButtonText: ST_YESDELETEIT,
    closeOnConfirm: false },
    function () {
      $.ajax({
        type: 'POST',
        url: urlItem,
        success: function () {
          if (reDirect) {
            window.location.href = reDirect;
          }else{
            location.reload();
          }
        },
        error: function (error) {
          console.log(error);
        }
      });
    });

};

'use strict';
$(document).ready(function(){
  $('.deleteItem').on('click', function(){

    var urlItem = $(this).data('url');
    var reDirect = $(this).data('redirect'); // Redirect page name after delete e.g. "items"

    deleteAlert(urlItem, reDirect);

  });
});

'use strict';
$(document).ready(function(){
  $('#table_id tbody').on('click', '.deleteItem', function(){

    var urlItem = $(this).data('url');
    var reDirect = $(this).data('redirect'); // Redirect page name after delete e.g. "items"

    deleteAlert(urlItem, reDirect);

  });
});

//------------------------------------------------

'use strict';
$(document).ready(function()
{
 $(".city").change(function()
 {
  var city_id=$(this).val();
  var dataString = 'city_id='+ city_id;

  $.ajax
  ({
   type: "POST",
   url: "get_zones.php",
   data: dataString,
   cache: false,
   success: function(html)
   {
    $(".zone").html(html);
  } 
});
});
 
});

//------------------------------------------------

'use strict';
var ul_sortable = $('.sortable');
ul_sortable.sortable({
  revert: 100,
  placeholder: 'placeholder'
});
ul_sortable.disableSelection();
var btn_save = $('button.save'),
div_response = $('#response');
btn_save.on('click', function(e) {
  e.preventDefault();
  var menuId = $(this).data('id');
  var sortable_data = ul_sortable.sortable('serialize');
  div_response.text(ST_PROCESSING);
  $.ajax({
    data: sortable_data,
    type: 'POST',
    url: 'save_navigation.php?menu='+menuId,
    success:function(result) {
      div_response.text(result);
    },
    error:function (result){
      console.log(result);
    }
  });
});

//------------------------------------------------

'use strict';
function checkAgentEmailAvailability() {
  jQuery.ajax({
    url: "../controller/check_agent_email.php",
    data:'agent_email='+$("#agent_email").val(),
    type: "POST",
    success:function(data){
      $("#email-availability-status").html(data);
    },
    error:function (){}
  });
}

//------------------------------------------------

'use strict';
$(".toggle-password").click(function() {

  $(this).toggleClass("fa-eye fa-eye-slash");
  var input = $($(this).attr("toggle"));
  if (input.attr("type") == "password") {
    input.attr("type", "text");
  } else {
    input.attr("type", "password");
  }
});

//------------------------------------------------

'use strict';
function checkUsernameAvailability() {
  jQuery.ajax({
    url: "../controller/check_username.php",
    data:'manager_username='+$("#manager_username").val(),
    type: "POST",
    success:function(data){
      $("#user-availability-status").html(data);
    },
    error:function (){}
  });
}

//------------------------------------------------

'use strict';
function checkEmailAvailability() {
  jQuery.ajax({
    url: "../controller/check_email.php",
    data:'manager_email='+$("#manager_email").val(),
    type: "POST",
    success:function(data){
      $("#email-availability-status").html(data);
    },
    error:function (){}
  });
}

//------------------------------------------------

'use strict';
$(document).ready(function(){
  $(".m_switch_check:checkbox").mSwitch({
    onRender:function(elem){
      var entity = elem.attr("entity");
      var label = elem.parent().parent().prev(".m_settings_label");
      if (elem.val() == 0){
        $.mSwitch.turnOff(elem);
        label.html(entity + " <span class=\"m_red\">(Disable)</font>");
      }else{
        label.html(entity + " <span class=\"m_green\">(Enable)</font>");
        $.mSwitch.turnOn(elem);
      }
    },
    onRendered:function(elem){
      /*console.log(elem);*/
    },
    onTurnOn:function(elem){
      var entity = elem.attr("entity");
      var label = elem.parent().parent().prev(".m_settings_label");
      if (elem.val() == "0"){
        elem.val("1");
        label.html(entity + " <span class=\"m_green\">(Enable)</font>");
      }else{
        label.html(entity + " <span class=\"m_red\">(Error)</font>");
      }
    },
    onTurnOff:function(elem){
      var entity = elem.attr("entity");
      var label = elem.parent().parent().prev(".m_settings_label");
      if (elem.val() == 1){
        elem.val("0");
        label.html(entity + " <span class=\"m_red\">(Disable)</font>");
      }else{
        label.html(entity + " <span class=\"m_red\">(Error)</font>");
      }
    }
  });
});

//------------------------------------------------

'use strict';
$(document).ready(function(){
  $(".image-radio").each(function(){
    if($(this).find('input[type="radio"]').first().attr("checked")){
      $(this).addClass('image-radio-checked');
    }else{
      $(this).removeClass('image-radio-checked');
    }
  });

    // sync the input state
    $(".image-radio").on("click", function(e){
      $(".image-radio").removeClass('image-radio-checked');
      $(this).addClass('image-radio-checked');
      var $radio = $(this).find('input[type="radio"]');
      $radio.prop("checked",!$radio.prop("checked"));

      e.preventDefault();
    });
  });

//------------------------------------------------

'use strict';
$(document).ready(function() {

  // enable fileuploader plugin
  $('input[name="files"]').fileuploader({
    addMore: true
  });
  
});

'use strict';
$(function () {
  $(".selectDrop").select2();
});

'use strict';
$(document).ready(function(){
  $('[data-toggle="tooltip"]').tooltip();
});

// color picker

$(function() {
  $('#primary-color-picker').colorpicker().on('changeColor', function(e) {
    $('#primary-color-preview')[0].style.backgroundColor = e.color
    .toString('rgba');
  });
});

$(function() {
  $('#secondary-color-picker').colorpicker().on('changeColor', function(e) {
    $('#secondary-color-preview')[0].style.backgroundColor = e.color
    .toString('rgba');
  });
});


