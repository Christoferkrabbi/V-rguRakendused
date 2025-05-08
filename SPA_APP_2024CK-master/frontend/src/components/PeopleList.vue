<template>
    <button @click="CreatePerson">Lisa Isik</button>
    <PeopleList :title="title" />
    <div class="about">
        <h1>Peoplelist</h1>
        <div class="peoplelist">
            <DataTable :value="people" v-if="people.length > 0 ">
                <Column field="id" header="Inimese id" style="color: black; " />
                <Column field="age" header="Inimese vanus" style="color: black; " />
                <Column field="Name" header="Inimese nimi" style="color: black; " />
            </DataTable>
            <div v-else>Inimesed puuduvad</div>
        </div>
    </div>
</template>


<script setup lang="ts">
import { type Person } from '@/models/person';
import { usePeopleStore } from "@/stores/peopleStore";
import { storeToRefs } from "pinia";
import { defineProps, onMounted, watch, ref  } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

watch(route, (to, from) => {
  if (to.path !== from.path || to.query !== from.query) {
    peopleStore.load();
  }
}, { deep: true });

defineProps<{ title: String }>();
const peopleStore = usePeopleStore();
const { people } = storeToRefs(peopleStore);

onMounted(async () => {
  peopleStore.load();
});
</script>
<style>

</style>