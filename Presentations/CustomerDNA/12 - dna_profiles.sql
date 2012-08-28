select 
p.BackerId,
s.similar_count, 
state_successful,
state_failed,
category_board_card_games,
category_video_games,
category_comics,
category_games,
category_product_design,
category_technology,
goal_1_1000,
goal_1001_5000,
goal_5001_10000,
goal_10001_30000,
goal_30001_100000,
goal_100001_250000,
goal_250001_plus
from backer_similar_pass2_dedupe s, dna_backer p
where p.BackerId = s.BackerId
order by similar_count desc