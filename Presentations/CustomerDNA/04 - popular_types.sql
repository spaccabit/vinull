select * from backer_similar, backer_profile
where similar_count >= 1000
and backer_profile.BackerId = backer_similar.BackerId
order by similar_count desc