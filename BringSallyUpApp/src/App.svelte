<script>
	import Notifications from 'svelte-notifications';
	import LoginPage from './pages/LoginPage.svelte';
	import MainFeedPage from './pages/MainFeedPage.svelte';
	import { pageConstants } from './pages/PageConstants';
	import { CurrentPageStore } from './stores';

</script>

<main>
	<h1>Bring Sally Up! Challenge</h1>
    <hr/>
	{#if $CurrentPageStore === pageConstants.loginPage}
		<Notifications>
			<LoginPage />
		</Notifications>
	{/if}
	{#if $CurrentPageStore === pageConstants.mainFeedPage}
		<MainFeedPage />
	{/if}
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		margin-top: -20px;
		color: black;
		text-transform: uppercase;
		font-size: 2em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>