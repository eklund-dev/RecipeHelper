if ( $PSDefaultParameterValues["*:Context"] -ne $null ) {
	$PSDefaultParameterValues.Remove("*:Context")
}	else {
	$PSDefaultParameterValues.Add("*:Context", "RecipeHelperDbContext")
}

$PSDefaultParameterValues