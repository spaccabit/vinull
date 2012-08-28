select top 10 
p.state_successful_avg, b.state_successful_avg, b.state_successful_stdev,
p.state_successful_avg - b.state_successful_avg,
(p.state_successful_avg - b.state_successful_avg) / b.state_successful_stdev
from backer_profile p, dna_baseline b