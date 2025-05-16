<template> 
    <br />
  <EventList :title="title"/> 
    <br />
  <peopleList :title="title"/> 
</template>

<script setup lang="ts">
    import EventList from "@/components/EventList.vue"
    import PeopleList from "@/components/PeopleList.vue"
    import { defineProps } from "vue";

    defineProps<{ title: String }>();

  
</script>

