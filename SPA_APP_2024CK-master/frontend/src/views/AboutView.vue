<template>
    <EventList :title="title" />
    <PeopleList :title="title" />
</template>

<script setup lang="ts">
    import EventList from "@/components/EventList.vue";
    import PeopleList from "@/components/PeopleList.vue";
    import { defineProps } from "vue";
    
    define<{ title: String }>();
</script>
