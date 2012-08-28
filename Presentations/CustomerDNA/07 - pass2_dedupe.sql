select p1.BackerId
from backer_profile base, backer_profile p1
where p1.BackerId in (select BackerId from backer_similar_pass2_dedupe) 
and base.BackerId = 2394748
and base.BackerId != p1.BackerId
and p1.state_successful_avg between (base.state_successful_avg - base.state_successful_stdev) and (base.state_successful_avg + base.state_successful_stdev) 
and p1.state_failed_avg between (base.state_failed_avg - base.state_failed_stdev) and (base.state_failed_avg + base.state_failed_stdev) 
and p1.category_board_card_games_avg between (base.category_board_card_games_avg - base.category_board_card_games_stdev) and (base.category_board_card_games_avg + base.category_board_card_games_stdev)
and p1.category_video_games_avg between (base.category_video_games_avg - base.category_video_games_stdev) and (base.category_video_games_avg + base.category_video_games_stdev)
and p1.category_comics_avg between (base.category_comics_avg - base.category_comics_stdev) and (base.category_comics_avg + base.category_comics_stdev)
and p1.category_games_avg between (base.category_games_avg - base.category_games_stdev) and (base.category_games_avg + base.category_games_stdev)
and p1.category_product_design_avg between (base.category_product_design_avg - base.category_product_design_stdev) and (base.category_product_design_avg + base.category_product_design_stdev)
and p1.category_technology_avg between (base.category_technology_avg - base.category_technology_stdev) and (base.category_technology_avg + base.category_technology_stdev)
and p1.goal_1_1000_avg between (base.goal_1_1000_avg - base.goal_1_1000_stdev) and (base.goal_1_1000_avg + base.goal_1_1000_stdev)
and p1.goal_1001_5000_avg between (base.goal_1001_5000_avg - base.goal_1001_5000_stdev) and (base.goal_1001_5000_avg + base.goal_1001_5000_stdev)
and p1.goal_5001_10000_avg between (base.goal_5001_10000_avg - base.goal_5001_10000_stdev) and (base.goal_5001_10000_avg + base.goal_5001_10000_stdev)
and p1.goal_10001_30000_avg between (base.goal_10001_30000_avg - base.goal_10001_30000_stdev) and (base.goal_10001_30000_avg + base.goal_10001_30000_stdev)
and p1.goal_30001_100000_avg between (base.goal_30001_100000_avg - base.goal_30001_100000_stdev) and (base.goal_30001_100000_avg + base.goal_30001_100000_stdev)
and p1.goal_100001_250000_avg between (base.goal_100001_250000_avg - base.goal_100001_250000_stdev) and (base.goal_100001_250000_avg + base.goal_100001_250000_stdev)
and p1.goal_250001_plus_avg between (base.goal_250001_plus_avg - base.goal_250001_plus_stdev) and (base.goal_250001_plus_avg + base.goal_250001_plus_stdev)
