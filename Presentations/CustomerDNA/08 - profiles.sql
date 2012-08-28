select 
p.BackerId,
p.project_count,
s.similar_count, 
state_successful_avg,
state_failed_avg,
category_board_card_games_avg,
category_video_games_avg,
category_comics_avg,
category_games_avg,
category_product_design_avg,
category_technology_avg,
goal_1_1000_avg,
goal_1001_5000_avg,
goal_5001_10000_avg,
goal_10001_30000_avg,
goal_30001_100000_avg,
goal_100001_250000_avg,
goal_250001_plus_avg
from backer_similar_pass2_dedupe s, backer_profile p
where p.BackerId = s.BackerId
order by similar_count desc