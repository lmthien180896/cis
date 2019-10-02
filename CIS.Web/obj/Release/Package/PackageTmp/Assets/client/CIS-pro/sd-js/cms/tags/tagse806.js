current_id = 0;
max_ids = 1;
var mylangs =new Array();
var myinput_ids = new Array();
count_langs = 0;
start_var = 1;

function monkeyPatchAutocomplete() {

          var oldFn = jQuery.ui.autocomplete.prototype._renderItem;

          jQuery.ui.autocomplete.prototype._renderItem = function( ul, item) {

              var t = item.label.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + $.ui.autocomplete.escapeRegex(this.term) + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<span style='font-weight:bold;color:#fff;'>$1</span>");

						
              return jQuery( "<li></li>" )
                  .data( "item.autocomplete", item )
                  .append( "<a>" + t + "</a>" )
                  .appendTo( ul );
          };
      }

function deleteTagBox(id,parentid){

	jQuery("p#tag_box_"+id+"_"+parentid).remove();
	
	mylangs[parentid]--;


}

function addSuggest(id,parentid){

	jQuery("#tag_"+id+"_"+parentid).autocomplete({
			appendTo: "#import_search_form", 
		 	width: 100,
		    selectFirst: false,
		    minChars:false,
		    matchContains: true,
		    cacheLength: false,  
			source: function( request, response ) {
  				url = "/admin/structure/tags/tags.php?lang_id="+parentid+"&input=" + request.term;
          
					//alert(url);
                jQuery.getJSON(url+ "&callback=?",function(data) {
				
                    response(data);

                });
            }


	});


}




function addTagBox(id,parentid){
		

		new_id = 1;
		start_var = start_var+new_id;
		new_id = start_var;



		if(new_id == 1){

				new_id = mylangs[new_id++];
	
		}

		html_var = '<p id="tag_box_'+new_id+'_'+parentid+'"><label class="indent">&nbsp;</label><input type="text" size="25" class="long" value="" name="tag_'+parentid+'['+new_id+']" id="tag_'+new_id+'_'+parentid+'"" class="" autocomplete="off"/>';	
		html_var+= '<span class="minus" onclick="javascript:deleteTagBox('+new_id+','+parentid+')">&nbsp;<img src="/images/platform/admin/bin.gif"  style="vertical-align:middle;" alt="delete" /></span>';				
		html_var+= '</p>';
		
		jQuery('#tagging_'+parentid).before().append(html_var);
		html_var = "";


	addSuggest(new_id,parentid);


	
}




jQuery(document).ready(function(){
   	
	monkeyPatchAutocomplete();

	count_langs = jQuery('.tabbercontrol a ').length;

	for(i = 1; i <= count_langs; i++){

		mylangs[i] = jQuery('#tagging_'+i+' input').length;
        myinput_ids[i] = 2;
     
	}

	jQuery('.add_tag').click(function(){
	
			parentid = jQuery(this).attr('rel');
	
			id = jQuery(this).attr('title');
			addTagBox(id,parentid);
			

	});

	jQuery('.minus').click(function(){

			parentid = jQuery(this).parent().parent().attr('id');
			id = jQuery(this).attr('title');
			deleteTagBox(id,parentid);
		
	});



	jQuery('.tagging_area input').each(function(){
				
		
		id = jQuery(this).attr('id').split('_');
		start_var = parseInt(id[1]);
		parent_id = id[2];
		addSuggest(start_var,parent_id);
	
	
	});


});
