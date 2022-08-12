<script>
    import { UserIdStore } from "../stores";
    import { Tabs, Tab, TabList, TabPanel} from 'svelte-tabs';
    import AttemptInput from '../components/AttemptInput.svelte';
    import AttemptList from "../components/AttemptList.svelte";
</script>

<iframe
    title="Bring Sally Up"
    width="916"
    height="515"
    src="https://youtube.com/embed/41N6bKO-NVI">
</iframe>

<AttemptInput />
<div class='tabs'>
    <Tabs>
        <TabList>
            <Tab>All participants</Tab>
            <Tab>Just me</Tab>
        </TabList>

        <TabPanel>
            <AttemptList />
        </TabPanel>
        <TabPanel>
            <AttemptList userId={$UserIdStore} />
        </TabPanel>
    </Tabs>
</div>
<div class='space'>

</div>

<style>
    .tabs {
        margin-top: 20px
    }
    .space {
        height: 500px;
    }
</style>