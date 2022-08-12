//business logic
function markCompleted(checkbox) {
  let form = checkbox.closest('form');
  form.submit();
}

$(document).ready(function() {

  $('.itemCheckbox').on('click', function (e) {
    e.preventDefault();
    markCompleted(e.target);

    
  });
});