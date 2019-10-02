rel = 0;
		tabs = 0;
		selectedtab = 0;

		
		function displaytab(selectedtab){

				jQuery('.tabbertab').hide();
				jQuery('.tabbertab').eq(selectedtab).show();
				jQuery('.tabbercontrol a').removeClass('selected');
				jQuery('.tabbercontrol a').eq(selectedtab).addClass('selected');


		}

		jQuery(document).ready(function(){
			
			tabs = jQuery('.tabbertab').length;

			if(tabs > 1){
					
				htmlinsert = "<div class='tabbercontrol'>";

				jQuery('.tabbertab').each(function(){

					navoptions = jQuery(this).attr('title');
					htmlinsert = htmlinsert+"<a rel='"+rel+"' title='"+navoptions+"'>"+navoptions+"</a>";
					rel++;

				});

				htmlinsert = htmlinsert+"</div><div class='clear'></div>";

				jQuery(htmlinsert).insertBefore('.tabber');;
			
				displaytab(selectedtab);

				jQuery('.tabbercontrol a').click(function(){


					selectedtab = jQuery(this).attr('rel');
							
					displaytab(selectedtab);

				});

			


			}

			

		});
